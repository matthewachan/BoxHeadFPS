using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierControl : MonoBehaviour {
    private UnityEngine.UI.Text m_MultiplierText;
    private UnityEngine.UI.Image m_DotImg;
    


    private int m_CurrentMultiplier;
    private int m_MinMultiplier;



	// Use this for initialization
	void Start () {
        m_MinMultiplier = 1;
        m_CurrentMultiplier = m_MinMultiplier;

        m_MultiplierText = gameObject.GetComponent<UnityEngine.UI.Text>();
        UpdateMultiplier();
	}
	
    public void IncrementMultiplier() {
        ++m_CurrentMultiplier;
        UpdateMultiplier();
    }

    void UpdateMultiplier() {
        m_MultiplierText.text = "x" + m_CurrentMultiplier;
    }

    public int GetMultiplier() {
        return m_CurrentMultiplier;
    }
	// Update is called once per frame
	//void Update () {
		
	//}
}
