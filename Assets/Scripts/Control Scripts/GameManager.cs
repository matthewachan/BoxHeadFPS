using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform m_LootSpawn;
    [SerializeField] private GameObject m_LootBoxPrefab;
    [SerializeField] private GameObject m_DevilPrefab;
    [SerializeField] private GameObject m_ZombiePrefab;
    private Transform m_EnemySpawn;

    private EnemyManager m_Devil;
    
    private Lootbox m_LootBox;
    private bool m_IsGameOver;
    private bool m_CanSpawn;
    private int m_SpawnTimer;

	// Use this for initialization
	void Start () {
        m_IsGameOver = false;
        m_CanSpawn = true;
        m_SpawnTimer = 15;

        m_EnemySpawn = GameObject.Find("EnemySpawn").GetComponent<Transform>();


        StartCoroutine(SpawnWave(m_ZombiePrefab, 25, m_EnemySpawn.position, m_EnemySpawn.rotation, false, 50, 10, 1, 1, 1, .1f, 1));

        m_LootBox = Instantiate(m_LootBoxPrefab, m_LootSpawn.position, m_LootSpawn.rotation).GetComponent<Lootbox>();
        m_LootBox.Initialize("UZI");
        
	}

    EnemyManager SpawnEnemy(GameObject prefab, Vector3 spawnLocation, Quaternion rotation, bool isDevil, int maxHP, int attackDamage, float attackRange, int moveSpeed, int turnSpeed, float knockbackDist, float attackCD) {
        EnemyManager enemy = Instantiate(prefab, spawnLocation, rotation).GetComponent<EnemyManager>();
        enemy.Initialize(isDevil, maxHP, attackDamage, attackRange, moveSpeed, turnSpeed, knockbackDist, attackCD);
        StartCoroutine(SpawnCooldown(1));
        return enemy;
    }

    IEnumerator SpawnWave(GameObject prefab, int number, Vector3 spawnLocation, Quaternion rotation, bool isDevil, int maxHP, int attackDamage, float attackRange, int moveSpeed, int turnSpeed, float knockbackDist, float attackCD) {
        List<EnemyManager> enemies = new List<EnemyManager>();
        
        for (int i = 0; i < number; ++i) {
            enemies.Add(SpawnEnemy(prefab, spawnLocation, rotation, isDevil, maxHP, attackDamage, attackRange, moveSpeed, turnSpeed, knockbackDist, attackCD));
            yield return new WaitForSeconds(0.8f);
        }

        //return enemies;
    }

    private void Update() {
        // Pause game
        if (m_IsGameOver)
            Time.timeScale = 0;
    }


    IEnumerator SpawnCooldown(float delay) {
        m_CanSpawn = false;
        yield return new WaitForSeconds(delay);
        m_CanSpawn = true;
    }

    public void IsGameOver(bool flag) {
        m_IsGameOver = flag;
    }

    public bool IsGameOver() {
        return m_IsGameOver;
    }



}
