using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour {
    private GameObject m_Player;
    private PlayerHealth m_PlayerHealth;
    private MultiplierControl m_Multiplier;
    private ScoreControl m_Scoreboard;
    private int m_CurrentHealth;
    private int m_MaxHealth;
    private int m_AttackDamage;
    private int m_TurnSpeed;
    private int m_BaseKillPts;
    private int m_MoveSpeed;
    private float m_AttackCooldown;
    private float m_KnockbackDist;
    private float m_DisappearDelay;
    private bool m_CanAttack;
    private bool m_IsAlive;
    private bool m_IsLimp;

	// Use this for initialization
	void Start () {
        m_Player = GameObject.Find("Player");
        m_PlayerHealth = GameObject.Find("Health Bar").GetComponent<PlayerHealth>();
        m_Multiplier = GameObject.Find("Multiplier").GetComponent<MultiplierControl>();
        m_Scoreboard = GameObject.Find("Score").GetComponent<ScoreControl>();        
        m_IsAlive = true;
        m_IsLimp = false;
        m_MaxHealth = 100;
        m_CurrentHealth = m_MaxHealth;
        m_AttackDamage = 10;
        m_AttackCooldown = 1;
        m_CanAttack = true;
        m_TurnSpeed = 1;
        m_MoveSpeed = 1;
        m_KnockbackDist = 1;
        m_DisappearDelay = 2;
        m_BaseKillPts = 100;
    }

    private void Update() {
        if (m_CurrentHealth <= 0) {
            if (!m_IsLimp) {

                m_IsAlive = false;
                m_CanAttack = false;

                // Score keeping
                m_Scoreboard.IncrementScore(m_BaseKillPts * m_Multiplier.GetMultiplier());
                m_Multiplier.IncrementMultiplier();

                // Enemy keels over
                transform.Rotate(new Vector3(-90, 0));
                m_IsLimp = true;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;

                Destroy(gameObject, m_DisappearDelay);
            }
        }
    }


    void FixedUpdate() {
        if (m_IsAlive) {
            // Follow player
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(m_Player.transform.position - transform.position), m_TurnSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(new Vector3(0f, transform.rotation.eulerAngles.y, 0f));
            transform.position += transform.forward * m_MoveSpeed * Time.deltaTime;
        }
    }

    void Attack() {
        m_PlayerHealth.TakeDamage(m_AttackDamage);
        StartCoroutine(Cooldown());
    }
    void Knockback() {
        transform.Translate(new Vector3(0, 0, -m_KnockbackDist));
    }
    IEnumerator Cooldown() {
        m_CanAttack = false;
        yield return new WaitForSeconds(m_AttackCooldown);
        m_CanAttack = true;
    }

    private void OnTriggerStay(Collider player) {
        if (player.name == "Player" && m_CanAttack)
            Attack();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.tag == "Bullet" && m_IsAlive) {
            m_CurrentHealth -= m_Player.GetComponent<WeaponsControl>().GetCurrentWeapon().GetAttackDamage();
            Knockback();
            Debug.Log(m_CurrentHealth);
        }
    }

    

    



}
