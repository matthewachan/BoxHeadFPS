using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Handles all of the Enemy vars
public class EnemyManager : MonoBehaviour {
    

    //private EnemyMovement m_Movement;
    //private EnemyBattle m_Battle;
    private GameObject m_Player;
    private PlayerHealth m_PlayerHealth;

    private bool m_IsAlive;

    // Movement 
    private int m_TurnSpeed;
    private int m_BaseKillPts;
    private int m_MoveSpeed;
    private float m_KnockbackDist;
    private float m_DisappearDelay;
    private bool m_IsDevil;



    // Battle
    private int m_CurrentHealth;
    private int m_MaxHealth;
    private int m_AttackDamage;
    private float m_AttackCooldown;
    private float m_AttackRange;

    private bool m_CanAttack;
    private bool m_IsLimp;



    // Use this for initialization
    void Start () {
        //m_Movement = GetComponent<EnemyMovement>();
        //m_Battle = GetComponent<EnemyBattle>();
        m_IsAlive = true;
	}

    // Ghetto constructor
    public void Initialize(bool isDevil, int maxHP, int attackDamage, float attackRange, int moveSpeed, int turnSpeed, float knockbackDist, float attackCD) {
        m_IsDevil = isDevil;
        m_TurnSpeed = turnSpeed;
        m_MoveSpeed = moveSpeed;
        m_KnockbackDist = knockbackDist;
        m_AttackDamage = attackDamage;
        m_AttackRange = attackRange;
        m_MaxHealth = maxHP;
        m_CurrentHealth = maxHP;
        m_AttackCooldown = attackCD;

        // Update attack range
        if (m_IsDevil) {
            GetComponent<BoxCollider>().size = new Vector3(m_AttackRange, 2, m_AttackRange);
            m_BaseKillPts = 1000;
        }
        else {
            GetComponent<BoxCollider>().size = new Vector3(1, .5f, m_AttackRange);
            m_BaseKillPts = 100;
        }
    }



    /* Movement mutators and Accessors */
    public int GetMoveSpeed() {
        return m_MoveSpeed;
    }
    
    public int GetTurnSpeed() {
        return m_TurnSpeed;
    }

    public float GetKnockbackDist() {
        return m_KnockbackDist;
    }


    /* Battle mutators and Accessors */
    public bool IsDevil() {
        return m_IsDevil;
    }

    public bool IsAlive() {
        return m_IsAlive;
    }

    public int GetMaxHealth() {
        return m_MaxHealth;
    }

    public int GetCurrentHealth() {
        return m_CurrentHealth;
    }

    public int GetAttackDamage() {
        return m_AttackDamage;
    }

    public float GetAttackCooldown() {
        return m_AttackCooldown;
    }
    
    public long GetBaseKillPts() {
        return m_BaseKillPts;
    }

    public void IsAlive(bool flag) {
        m_IsAlive = flag;
    }

    public void LoseHealth(int damage_taken) {
        m_CurrentHealth -= damage_taken;
    }

    public void SetMaxHealth(int max_hp) {
        m_MaxHealth = max_hp;
    }

    public void SetAttackDamage(int attack_damage) {
        m_AttackDamage = attack_damage;
    }

    public void SetAttackCooldown(float cooldown) {
        m_AttackCooldown = cooldown;
    }








}
