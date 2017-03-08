using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierControl : MonoBehaviour {
    private UnityEngine.UI.Text m_MultiplierText;
    private RectTransform m_DotPos;
    private UnityEngine.UI.Image m_DotImg;
    private Animation m_Anim;
    

    private int m_CurrentMultiplier;
    private int m_MinMultiplier;

    private float m_AnimSpeed;


    private Vector3 m_DefaultPos;

	// Use this for initialization
	void Start () {
        m_MinMultiplier = 1;
        m_CurrentMultiplier = m_MinMultiplier;

        m_MultiplierText = gameObject.GetComponent<UnityEngine.UI.Text>();

        m_DotImg = GameObject.Find("Dot").GetComponent<UnityEngine.UI.Image>();
        m_DotPos = GameObject.Find("Dot").GetComponent<RectTransform>();
        m_Anim = GameObject.Find("Dot").GetComponent<Animation>();
        m_Anim.wrapMode = WrapMode.Once;
        m_AnimSpeed = m_Anim["RampDown"].speed;

        // Set default position for slider
        m_DefaultPos = new Vector3(26, 35, 0);
        ResetSlider();



        // Init multiplier at x1
        UpdateMultiplier();
	}

    private void ResetSlider() {
        m_DotImg.color = Color.white;
        m_DotPos.localPosition = m_DefaultPos;
    }

    public void IncrementMultiplier() {
        ++m_CurrentMultiplier;
        AnimateSlider();
        UpdateMultiplier();
    }

    private void AnimateSlider() {
        m_Anim.Stop();
        m_Anim["RampDown"].speed = m_AnimSpeed * m_CurrentMultiplier/10.5f;
        m_Anim.Play();

    }

    public void DecrementMultiplier() {

        --m_CurrentMultiplier;
        ResetSlider();
        if (m_CurrentMultiplier > 1) { 
            
            AnimateSlider();
        }
        UpdateMultiplier();
    }

    void UpdateMultiplier() {
        m_MultiplierText.text = "x" + m_CurrentMultiplier;
    }

    public int GetMultiplier() {
        return m_CurrentMultiplier;
    }


    private void Update() {
        Debug.Log(m_Anim["RampDown"].speed);
        if (!m_Anim.IsPlaying("RampDown") && m_CurrentMultiplier > 1) {
            DecrementMultiplier();
        }

    }
}
