using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform m_LootSpawn;
    [SerializeField] private GameObject m_LootBoxPrefab;
    [SerializeField] private GameObject m_DevilPrefab;
    [SerializeField] private GameObject m_ZombiePrefab;
    private Transform m_EnemySpawn;
    private Transform m_EnemySpawn2;

    private EnemyManager m_Devil;
    
    private Lootbox m_LootBox;
    private bool m_IsGameOver;
    private float m_MinSpawnTime;
    private float m_MaxSpawnTime;

	// Use this for initialization
	void Start () {
        m_IsGameOver = false;

        m_MinSpawnTime = 1;
        m_MaxSpawnTime = 5;

        m_EnemySpawn = GameObject.Find("EnemySpawn").GetComponent<Transform>();
        m_EnemySpawn2 = GameObject.Find("EnemySpawn_2").GetComponent<Transform>();

        StartCoroutine(SpawnWave(m_ZombiePrefab, 25, Random.Range(m_MinSpawnTime, m_MaxSpawnTime), m_EnemySpawn.position, m_EnemySpawn.rotation, false, 30, 10, 1, 1, 1, .6f, 1));
        StartCoroutine(SpawnWave(m_DevilPrefab, 5, Random.Range(m_MinSpawnTime, m_MaxSpawnTime), m_EnemySpawn2.position, m_EnemySpawn2.rotation, true, 30, 20, 7, 2, 2, .6f, 2));
        

        m_LootBox = Instantiate(m_LootBoxPrefab, m_LootSpawn.position, m_LootSpawn.rotation).GetComponent<Lootbox>();

        
	}

    EnemyManager SpawnEnemy(GameObject prefab, Vector3 spawnLocation, Quaternion rotation, bool isDevil, int maxHP, int attackDamage, float attackRange, int moveSpeed, int turnSpeed, float knockbackDist, float attackCD) {
        EnemyManager enemy = Instantiate(prefab, spawnLocation, rotation).GetComponent<EnemyManager>();
        enemy.Initialize(isDevil, maxHP, attackDamage, attackRange, moveSpeed, turnSpeed, knockbackDist, attackCD);
        return enemy;
    }

    IEnumerator SpawnWave(GameObject prefab, int number, float spawnDelay, Vector3 spawnLocation, Quaternion rotation, bool isDevil, int maxHP, int attackDamage, float attackRange, int moveSpeed, int turnSpeed, float knockbackDist, float attackCD) {
        List<EnemyManager> enemies = new List<EnemyManager>();
        
        for (int i = 0; i < number; ++i) {
            enemies.Add(SpawnEnemy(prefab, spawnLocation, rotation, isDevil, maxHP, attackDamage, attackRange, moveSpeed, turnSpeed, knockbackDist, attackCD));
            yield return new WaitForSeconds(spawnDelay);
        }

        //return enemies;
    }

    public Lootbox SpawnLootbox(Vector3 position, Quaternion rotation) {
        GameObject lootbox = Instantiate(m_LootBoxPrefab, position, rotation);
        return lootbox.GetComponent<Lootbox>();
    }


    private void Update() {
        // Pause game
        if (m_IsGameOver)
            Time.timeScale = 0;
    }

    public void IsGameOver(bool flag) {
        m_IsGameOver = flag;
    }

    public bool IsGameOver() {
        return m_IsGameOver;
    }



}
