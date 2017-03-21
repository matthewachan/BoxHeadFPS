//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ExplosionControl : MonoBehaviour {
//    private List<GameObject> m_TrackedObjects;
    

//	// Use this for initialization
//	void Start () {
//	    m_TrackedObjects = new List<GameObject>();
//    }

//    private void OnTriggerEnter(Collider other) {
//        if (other.tag == "Zombie" || other.tag == "Devil" || other.tag == "Player") {
//          m_TrackedObjects.Add(other.gameObject);
//        }
//    }

//    private void OnTriggerExit(Collider other) {
//      // substitute this with one layer
//      if (other.tag == "Zombie" || other.tag == "Devil" || other.tag == "Player") {
//        m_TrackedObjects.Remove(other.gameObject);
//      }
//    }
    
//    // Called by ZombieCtrl when a Zombie attacks a barrel
//    public void Explode() {
//      // Damage all objects in m_TackedObjects
      
      
//      // Play explosion particle effect
//    }

//    private void OnCollisionEnter(Collision collision) {
//      if (this.tag == "Barrel") {
//        if (collision.gameObject.tag == "Bullet")
//          Explode();
      
//      } else if (this.tag == "Grenade") {
//        Invoke("Explode", 3); 
      
//      } else if (this.tag == "Claymore") {
//        if (other.tag == "Zombie" || other.tag == "Devil" || other.tag == "Player") 
//          Explode();
//      } else {
//     // Wait on PlayerCtrl to detonate Chargepack 
      
//      }
//    }

//}
