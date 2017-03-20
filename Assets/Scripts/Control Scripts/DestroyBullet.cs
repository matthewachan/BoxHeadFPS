using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {
	// Use this for initialization
	void Start () {
	}


    private void OnCollisionEnter(Collision collision) {
        if (collision.transform.name != "Player")
            Destroy(gameObject);
    }

}
