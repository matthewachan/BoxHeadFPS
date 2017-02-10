using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lootbox : MonoBehaviour {
    private string m_Weapon;
    private NotificationControl m_Notification;
    

	// Use this for initialization
	void Start () {
        m_Notification = GameObject.Find("Notification").GetComponent<NotificationControl>();
    }

    // Ghetto constructor
    public void Initialize(string weapon) {
        m_Weapon = weapon;
    }

    private void OnTriggerEnter(Collider player) {
        if (player.name == "Player") {
            GameObject.Find("Player").GetComponent<WeaponsControl>().UpdateInventory(m_Weapon);
            m_Notification.PostNotification("Picked up " + m_Weapon);
            Destroy(gameObject);   
        }
    }
}
