using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour {
    [SerializeField]
    private Rigidbody m_FireballPrefab;

    private Animator m_Anim;
    private int m_AttackHash;
    private int m_DeadHash;
    private GameObject m_Player;
    private PlayerHealth m_PlayerHealth;
    private ScoreControl m_Scoreboard;
    private MultiplierControl m_Multiplier;
    private Transform m_FireballSpawn;
    private GameManager m_GameManager;

    private int m_FireballSpeed;
    private int m_DisappearDelay;
    private bool m_IsLimp;
    private bool m_CanAttack;
    // Use this for initialization
    void Start () {
        m_Anim = GetComponent<Animator>();
        m_AttackHash = Animator.StringToHash("isAttacking");
        m_DeadHash = Animator.StringToHash("isDead");
        m_Player = GameObject.Find("Player");
        m_PlayerHealth = GameObject.Find("Health Bar").GetComponent<PlayerHealth>();

        m_Scoreboard = GameObject.Find("Score").GetComponent<ScoreControl>();
        m_Multiplier = GameObject.Find("Multiplier").GetComponent<MultiplierControl>();
        m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


        m_FireballSpeed = 150;
        m_DisappearDelay = 2;
        m_IsLimp = false;
        m_CanAttack = true;
    }


    // Update is called once per frame
    void Update () {

        if (GetComponent<EnemyManager>().GetCurrentHealth() <= 0) {
            if (!m_IsLimp) {

                GetComponent<EnemyManager>().IsAlive(false);

                // Stop animating
                m_Anim.SetBool(m_DeadHash, GetComponent<EnemyManager>().IsAlive());
                m_Anim.Stop();

                m_CanAttack = false;

                // Score keeping
                m_Scoreboard.IncrementScore(GetComponent<EnemyManager>().GetBaseKillPts() * m_Multiplier.GetMultiplier());
                m_Multiplier.IncrementMultiplier();

                // Disable physics
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;

                // Devil's have a chance to drop lootboxes
                if (GetComponent<EnemyManager>().IsDevil() && Random.Range(1, 100) > 50) {
                    Lootbox lootbox = m_GameManager.SpawnLootbox(transform.position + new Vector3(0, 0.1f, 0), transform.rotation);
                    lootbox.SetRespawnable(false);
                }
                // Enemy keels over
                transform.Rotate(new Vector3(-90, 0));
                m_IsLimp = true;
                

                

                Destroy(gameObject, m_DisappearDelay);
            }
        }
    }

    public void SetFireballSpawn(Transform transform) {
        m_FireballSpawn = transform;
    }

    void MeleeAttack() {
        // Update animator state machine
        m_Anim.SetTrigger(m_AttackHash);
        StartCoroutine(DelayedAttack());
        StartCoroutine(Cooldown());
        // Reset state
        
    }


    void ShootFireball() {
        // Animate fireball attack
        m_Anim.SetTrigger(m_AttackHash);

        StartCoroutine(DelayedFireball());
        StartCoroutine(Cooldown());
    }

    void TakeDamage() {
        GetComponent<EnemyManager>().LoseHealth(m_Player.GetComponent<WeaponsControl>().GetCurrentWeapon().GetAttackDamage());
        GetComponent<EnemyMovement>().Knockback();

    }



    IEnumerator Cooldown() {

        m_CanAttack = false;
        yield return new WaitForSeconds(GetComponent<EnemyManager>().GetAttackCooldown());
            m_CanAttack = true;
    }

    IEnumerator DelayedAttack() {
        yield return new WaitForSeconds(.7f);
        m_PlayerHealth.TakeDamage(GetComponent<EnemyManager>().GetAttackDamage());

    }

    IEnumerator DelayedFireball() {
        yield return new WaitForSeconds(.8f);
        m_FireballSpawn = transform.GetChild(2);
        Rigidbody fireball = (Rigidbody)Instantiate(m_FireballPrefab, m_FireballSpawn.position, m_FireballSpawn.rotation);
        fireball.GetComponent<DestroyFireball>().SetDamage(GetComponent<EnemyManager>().GetAttackDamage());
        // Shoot fireball at player
        fireball.rotation = Quaternion.Slerp(fireball.rotation, Quaternion.LookRotation(m_Player.transform.position, fireball.position), Time.deltaTime);
        fireball.rotation = Quaternion.Euler(new Vector3(0f, fireball.rotation.eulerAngles.y, 0f));
        fireball.velocity += fireball.transform.forward * m_FireballSpeed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider player) {
        if (player.name == "Player" && m_CanAttack && !GameObject.Find("GameManager").GetComponent<GameManager>().IsGameOver()) {
            
            if (GetComponent<EnemyManager>().IsDevil() == false)
                MeleeAttack();
            else 
                ShootFireball();
            
            
        }
    }


    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Bullet" && GetComponent<EnemyManager>().IsAlive()) {
            TakeDamage();
        }
    }



}
