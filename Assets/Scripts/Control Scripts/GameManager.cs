using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform m_LootSpawn;
    [SerializeField] private GameObject m_LootBoxPrefab;

    private Lootbox m_LootBox;



	// Use this for initialization
	void Start () {
        m_LootBox = Instantiate(m_LootBoxPrefab, m_LootSpawn).GetComponent<Lootbox>();
        m_LootBox.Initialize("UZI");
        
	}
	
	// Update is called once per frame
	//void Update () {
		
	//}

}
