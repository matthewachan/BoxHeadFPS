using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionControl : MonoBehaviour {
    private ParticleSystem m_ParticleSystem;
    private bool m_Exploded;
    private List<GameObject> m_InRange;
    private int m_Damage;

    private void Awake() {
        m_Exploded = false;
        m_InRange = new List<GameObject>();
        m_Damage = 30;
    }
    
	// Use this for initialization
	void Start () {

        m_ParticleSystem = GetComponentInChildren<ParticleSystem>();
        m_ParticleSystem.loop = false;
        m_ParticleSystem.Stop();

        if (this.tag == "Grenade")
            Invoke("Explode", 2f);
	}

    public void Explode() {
        m_Exploded = true;
        m_ParticleSystem.Play();
        for (int i = 0; i < m_InRange.Count; ++i) {
            if (m_InRange[i].tag == "Player")
                GameObject.Find("Health Bar").GetComponent<PlayerHealth>().TakeDamage(m_Damage);
            else if (m_InRange[i].tag == "Enemy")
                m_InRange[i].GetComponent<EnemyManager>().LoseHealth(m_Damage);
            else if (m_InRange[i].tag == "Barrel" && !m_InRange[i].GetComponent<ExplosionControl>().HasExploded())
                m_InRange[i].GetComponent<ExplosionControl>().Explode();
                
        }
        if (gameObject.tag == "Barrel")
            GetComponent<Renderer>().enabled = false;
        else if (tag == "Claymore") {
            GetComponent<Blinking>().StopBlinking();
            GetComponent<Renderer>().enabled = false;
        } else if (tag == "Chargepack") {
            GetComponent<Blinking>().StopBlinking();
            GetComponent<Renderer>().enabled = false;
        }
        Destroy(gameObject, 2);
        
    }


    public bool HasExploded() {
        return m_Exploded;
    }

    private void OnTriggerEnter(Collider other) {
        
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Barrel") && !m_InRange.Contains(other.gameObject) && !other.isTrigger) {
            m_InRange.Add(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other) {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Barrel") && !m_InRange.Contains(other.gameObject) && !other.isTrigger) {
            m_InRange.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other) {
        if ((other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy" || other.gameObject.tag == "Barrel") && m_InRange.Contains(other.gameObject) && !other.isTrigger) {
            m_InRange.Remove(other.gameObject);
        }
    }

    private void Update() {
      //  Debug.Log(m_InRange.Count);
    }

    private void OnCollisionEnter(Collision collision) {
        
        if (gameObject.tag == "Barrel" && collision.gameObject.tag == "Bullet")
            Explode();
        else if (gameObject.tag == "Claymore" && collision.gameObject.tag == "Enemy") {
            GetComponent<Blinking>().ActivateBomb();
            Invoke("Explode", 2);
        }
    }
}
