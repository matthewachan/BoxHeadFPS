using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {
    UnityEngine.UI.Button m_SinglePlayBtn;
    UnityEngine.UI.Button m_CooperativeBtn;
    UnityEngine.UI.Button m_DeathMatchBtn;
    UnityEngine.UI.Button m_InstructionsBtn;
    UnityEngine.UI.Button[] buttons;

    // Use this for initialization
    void Start () {
        buttons = new UnityEngine.UI.Button[4];
        buttons[0] = m_SinglePlayBtn;
        buttons[1] = m_CooperativeBtn;
        buttons[2] = m_DeathMatchBtn;
        buttons[3] = m_InstructionsBtn;
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void LoadSinglePlay() {
        Debug.Log("single play");
        

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void LoadCooperative() {
        Debug.Log("co-op");
    }

    public void LoadDeathMatch() {
        Debug.Log("dm");
    }

    public void LoadInstructions() {
        Debug.Log("instructions");
    }
}
