using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbox : MonoBehaviour {
    private NotificationControl m_Notification;
    private WeaponsControl m_WeaponsControl;
    private GameManager m_GameManager;

    private bool m_IsRespawnable;


    private void Awake() {
        m_IsRespawnable = true;
    }

    // Use this for initialization
    void Start () {
        m_Notification = GameObject.Find("Notification").GetComponent<NotificationControl>();
        m_WeaponsControl = GameObject.Find("Player").GetComponent<WeaponsControl>();
        m_GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    private void RespawnLootbox() {
        gameObject.SetActive(true);
    }

    public void SetRespawnable(bool flag) {
        
        m_IsRespawnable = flag;
    }
    
    private void OnTriggerEnter(Collider player) {
        if (player.name == "Player") {
            int length = m_WeaponsControl.GetUnlockedWeapons().Count;
            string weaponName = m_WeaponsControl.KeyToName(Mathf.FloorToInt(Random.Range(0, length - .01f)));
            GameObject.Find("Player").GetComponent<WeaponsControl>().UpdateInventory(weaponName);
            m_Notification.PostNotification("Picked up " + weaponName);
            if (m_IsRespawnable) {
                gameObject.SetActive(false);
                Invoke("RespawnLootbox", 5);
            }
            else {
                Destroy(gameObject);
            }
        }
    }
}
