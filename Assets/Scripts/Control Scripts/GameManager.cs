using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform m_LootSpawn;
    [SerializeField] private GameObject m_LootBoxPrefab;
    [SerializeField] private GameObject m_DevilPrefab;
    private Transform m_EnemySpawn;

    private EnemyManager m_Devil;
    private Lootbox m_LootBox;
    private bool m_IsGameOver;


	// Use this for initialization
	void Start () {
        m_IsGameOver = false;

        m_EnemySpawn = GameObject.Find("EnemySpawn").GetComponent<Transform>();
        m_Devil = Instantiate(m_DevilPrefab, m_EnemySpawn).GetComponent<EnemyManager>();
        m_Devil.Initialize(true, 20, 20, 30, 2, 2, .1f, 2.5f);

        m_LootBox = Instantiate(m_LootBoxPrefab, m_LootSpawn).GetComponent<Lootbox>();
        m_LootBox.Initialize("UZI");
        
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
