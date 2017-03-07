using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFireball : MonoBehaviour {
    private int m_BulletDamage;

    // Use this for initialization
    void Start () {
        Destroy(gameObject, 5);
	}

    public void SetDamage(int damage) {
        m_BulletDamage = damage;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.name == "Player") {
            GameObject.Find("Health Bar").GetComponent<PlayerHealth>().TakeDamage(m_BulletDamage);
        }
        Destroy(gameObject);

    }
}
