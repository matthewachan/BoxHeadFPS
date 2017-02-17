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
        m_Devil = SpawnEnemy(m_DevilPrefab, m_EnemySpawn);
        m_Devil.Initialize(true, 20, 20, 30, 2, 2, .1f, 2.5f);
        StartCoroutine(SpawnCooldown(m_SpawnTimer));
        
        SpawnEnemy(m_ZombiePrefab, m_EnemySpawn).Initialize(false, 10, 10, 5, 1, 1, .1f, 1);


        m_LootBox = Instantiate(m_LootBoxPrefab, m_LootSpawn).GetComponent<Lootbox>();
        m_LootBox.Initialize("UZI");
        
	}

    EnemyManager SpawnEnemy(GameObject prefab, Transform spawnLocation) {
        return Instantiate(prefab, spawnLocation).GetComponent<EnemyManager>();
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
