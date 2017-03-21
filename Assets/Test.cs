using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<ParticleSystem>().Stop();
        Invoke("Explode", 2);
	}
	

    private void Explode() {
        gameObject.GetComponent<ParticleSystem>().Play();
    }
}
