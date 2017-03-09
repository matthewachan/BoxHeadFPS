using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierControl : MonoBehaviour {
    private UnityEngine.UI.Text m_MultiplierText;
    private RectTransform m_DotPos;
    private UnityEngine.UI.Image m_DotImg;
    private Animation m_Anim;


    private WeaponsControl m_Weapons;

    private int m_CurrentMultiplier;
    private int m_MinMultiplier;
    private int m_MaxMultiplier;

    private float m_AnimSpeed;


    private Vector3 m_DefaultPos;

	// Use this for initialization
	void Start () {
        m_MinMultiplier = 1;
        m_CurrentMultiplier = m_MinMultiplier;
        m_MaxMultiplier = m_CurrentMultiplier;

        // Grab components
        m_Weapons = GameObject.Find("Player").GetComponent<WeaponsControl>();
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
        UpdateMultiplierText();
	}


    private void Update() {
        if (!m_Anim.IsPlaying("RampDown") && m_CurrentMultiplier > 1) {
            DecrementMultiplier();
        }
    }

    private void AnimateSlider() {
        // Playback speed linearly increases with respect to current multiplier
        m_Anim.Stop();
        m_Anim["RampDown"].speed = m_AnimSpeed * m_CurrentMultiplier / 10.5f;
        m_Anim.Play();

    }

    private void ResetSlider() {
        m_DotImg.color = Color.white;
        m_DotPos.localPosition = m_DefaultPos;
    }

    public void IncrementMultiplier() {
        ++m_CurrentMultiplier;
        if (m_CurrentMultiplier > m_MaxMultiplier) {
            switch (m_CurrentMultiplier) {
                case 5:
                    m_Weapons.UnlockWeapon("UZI", 2);
                    break;
                case 10:
                    m_Weapons.UnlockWeapon("Shotgun", 3);
                    break;
                case 15:
                    m_Weapons.UnlockWeapon("Barrel", 4);
                    break;
                case 20:
                    m_Weapons.UnlockWeapon("Grenade", 5);
                    break;
                case 30:
                    m_Weapons.UnlockWeapon("Fake walls", 6);
                    break;
                case 40:
                    m_Weapons.UnlockWeapon("Claymore", 7);
                    break;
                case 50:
                    m_Weapons.UnlockWeapon("Rocket", 8);
                    break;
                case 55:
                    m_Weapons.UnlockWeapon("Chargepack", 9);
                    break;
                case 70:
                    m_Weapons.UnlockWeapon("Railgun", 0);
                    break;
                default:
                    break;

            }
            m_MaxMultiplier = m_CurrentMultiplier;
        }

        
        AnimateSlider();
        UpdateMultiplierText();
    }

    public void DecrementMultiplier() {
        --m_CurrentMultiplier;
        ResetSlider();
        if (m_CurrentMultiplier > 1) { 
            
            AnimateSlider();
        }
        UpdateMultiplierText();
    }

    void UpdateMultiplierText() {
        m_MultiplierText.text = "x" + m_CurrentMultiplier;
    }

    public int GetMultiplier() {
        return m_CurrentMultiplier;
    }


}
