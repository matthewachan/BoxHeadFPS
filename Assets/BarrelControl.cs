using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelControl : MonoBehaviour {
    private ParticleSystem m_ParticleSystem;
    private bool m_Exploded;
    private List<GameObject> m_InRange;
    private int m_Damage;

	// Use this for initialization
	void Start () {
        m_Exploded = false;
        m_InRange = new List<GameObject>();

        m_Damage = 30;

        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();
        m_ParticleSystem.loop = false;
        m_ParticleSystem.Stop();
	}

    public void Explode() {
        m_Exploded = true;
        m_ParticleSystem.Play();
        for (int i = 0; i < m_InRange.Count; ++i) {
            if (m_InRange[i].tag == "Player")
                GameObject.Find("Health Bar").GetComponent<PlayerHealth>().TakeDamage(m_Damage);
            else if (m_InRange[i].tag == "Enemy")
                m_InRange[i].GetComponent<EnemyManager>().LoseHealth(m_Damage);
            else if (m_InRange[i].tag == "Barrel" && !m_InRange[i].GetComponent<BarrelControl>().HasExploded())
                m_InRange[i].GetComponent<BarrelControl>().Explode();
                
        }
        GetComponent<Renderer>().enabled = false;
        Destroy(gameObject, 2);
        
    }


    public bool HasExploded() {
        return m_Exploded;
    }

    private void OnTriggerEnter(Collider other) {
        if ((other.tag == "Player" || other.tag == "Enemy" || other.tag == "Barrel") && !m_InRange.Contains(other.gameObject)) {
            m_InRange.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if ((other.tag == "Player" || other.tag == "Enemy" || other.tag == "Barrel") && !m_InRange.Contains(other.gameObject)) {
            m_InRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if ((other.tag == "Player" || other.tag == "Enemy" || other.tag == "Barrel") && m_InRange.Contains(other.gameObject)) {
            m_InRange.Remove(other.gameObject);
        }
    }

    private void Update() {
        Debug.Log(m_InRange.Count);
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Bullet") {
            Explode();
        }
    }
}
