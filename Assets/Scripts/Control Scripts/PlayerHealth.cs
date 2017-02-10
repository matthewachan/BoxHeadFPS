using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private RectTransform m_HealthBar;
    //[SerializeField] private UnityStandardAssets.Characters.FirstPerson.FirstPersonController m_Player;

    private NotificationControl m_Notification;
    private GameObject m_Player;
    private int m_CurrentHealth;
    private int m_MaxHealth;
    private int m_BarWidth;
    private Vector3 m_BarPos;
    private float m_KnockbackDist;

	// Use this for initialization
	void Start () {
        m_Notification = GameObject.Find("Notification").GetComponent<NotificationControl>();
        m_Player = GameObject.Find("Player");
        m_KnockbackDist = 1;
        m_MaxHealth = 100;
        m_CurrentHealth = m_MaxHealth;
        m_BarWidth = 150;
        m_BarPos = Vector3.zero;
	}


    public void TakeDamage(int damage) {
        m_CurrentHealth -= damage;
        Knockback();
    }

    void Knockback() {
        m_Player.transform.Translate(new Vector3(0, 0, -m_KnockbackDist));
    }

    public void Heal(int amount) {
        m_CurrentHealth += amount;
    }


    // Update is called once per frame
    void Update () {
        
        // Check character death state
        if (m_CurrentHealth <= 0) {
            m_Notification.PostNotification("You have died!");
            m_Player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            m_Player.GetComponent<WeaponsControl>().enabled = false;
        } else {
            // currentHealth--;
            m_BarPos.Set(-m_BarWidth + (m_CurrentHealth * m_BarWidth / m_MaxHealth), 0, 0);
            m_HealthBar.localPosition = m_BarPos;
        }

        

	}


}
