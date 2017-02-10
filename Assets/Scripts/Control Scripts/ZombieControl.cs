using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieControl : MonoBehaviour {
    private GameObject m_Player;
    private PlayerHealth m_PlayerHealth;
    private int m_CurrentHealth;
    private int m_MaxHealth;
    private int m_AttackDamage;
    private int m_TurnSpeed;
    
    private int m_MoveSpeed;
    private float m_AttackCooldown;
    private float m_KnockbackDist;
    private bool m_CanAttack;
    private bool m_IsAlive;
    private bool m_IsLimp;

	// Use this for initialization
	void Start () {
        m_Player = GameObject.Find("Player");
        m_PlayerHealth = GameObject.Find("Health Bar").GetComponent<PlayerHealth>();
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
    }

    private void Update() {
        if (m_CurrentHealth <= 0) {
            m_IsAlive = false;
            m_CanAttack = false;
            if (!m_IsLimp) {
                transform.Rotate(new Vector3(-90, 0));
                m_IsLimp = true;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;
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
            m_CurrentHealth -= m_Player.GetComponent<WeaponsControl>().getCurrentWeapon().getAttackDamage();
            Knockback();
            Debug.Log(m_CurrentHealth);
        }
    }

    

    



}
