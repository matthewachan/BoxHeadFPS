using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinking : MonoBehaviour {
    [SerializeField] private Texture m_Off;
    [SerializeField] private Texture m_On;

    [SerializeField] private float m_BlinkFreq;


    // Use this for initialization
    void Start () {
        Invoke("TurnOff", m_BlinkFreq);
	}

    void TurnOff() {
        gameObject.GetComponentInChildren<Light>().enabled = false;
        gameObject.GetComponent<Renderer>().material.mainTexture = m_Off;
        Invoke("TurnOn", m_BlinkFreq);
    }

    void TurnOn() {
        gameObject.GetComponentInChildren<Light>().enabled = true;
        gameObject.GetComponent<Renderer>().material.mainTexture = m_On;
        Invoke("TurnOff", m_BlinkFreq);
    }

    public void ActivateBomb() {
        m_BlinkFreq = 0.1f;
    }

    public void StopBlinking() {
        gameObject.GetComponentInChildren<Light>().enabled = false;
        CancelInvoke();
    }

}
