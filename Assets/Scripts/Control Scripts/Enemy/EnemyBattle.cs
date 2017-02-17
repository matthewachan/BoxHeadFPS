﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattle : MonoBehaviour {
    [SerializeField]
    private Rigidbody m_FireballPrefab;

    private GameObject m_Player;
    private PlayerHealth m_PlayerHealth;
    private ScoreControl m_Scoreboard;
    private MultiplierControl m_Multiplier;
    private GameObject m_FireballSpawn;

    private int m_FireballSpeed;
    private int m_DisappearDelay;
    private bool m_IsLimp;
    private bool m_CanAttack;

    // Use this for initialization
    void Start () {

        m_Player = GameObject.Find("Player");
        m_PlayerHealth = GameObject.Find("Health Bar").GetComponent<PlayerHealth>();
        m_FireballSpawn = GameObject.Find("FireballSpawn");

        m_Scoreboard = GameObject.Find("Score").GetComponent<ScoreControl>();
        m_Multiplier = GameObject.Find("Multiplier").GetComponent<MultiplierControl>();


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
                m_CanAttack = false;

                // Score keeping
                m_Scoreboard.IncrementScore(GetComponent<EnemyManager>().GetBaseKillPts() * m_Multiplier.GetMultiplier());
                m_Multiplier.IncrementMultiplier();

                // Enemy keels over
                transform.Rotate(new Vector3(-90, 0));
                m_IsLimp = true;
                gameObject.GetComponent<CapsuleCollider>().enabled = false;

                Destroy(gameObject, m_DisappearDelay);
            }
        }
    }


    void MeleeAttack() {
        m_PlayerHealth.TakeDamage(GetComponent<EnemyManager>().GetAttackDamage());
        StartCoroutine(Cooldown());
    }


    void ShootFireball() {
        Rigidbody fireball = (Rigidbody)Instantiate(m_FireballPrefab, m_FireballSpawn.transform.position, m_FireballSpawn.transform.rotation);
        fireball.GetComponent<DestroyFireball>().SetDamage(GetComponent<EnemyManager>().GetAttackDamage());
        // Shoot fireball at player
        fireball.rotation = Quaternion.Slerp(fireball.rotation, Quaternion.LookRotation(m_Player.transform.position, fireball.position), Time.deltaTime);
        fireball.rotation = Quaternion.Euler(new Vector3(0f, fireball.rotation.eulerAngles.y, 0f));
        fireball.velocity += fireball.transform.forward * m_FireballSpeed * Time.deltaTime;

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