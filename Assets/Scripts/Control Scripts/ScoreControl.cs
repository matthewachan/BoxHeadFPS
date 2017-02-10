using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreControl : MonoBehaviour {
    private long m_CurrentScore;
    private UnityEngine.UI.Text m_Scoreboard;


	// Use this for initialization
	void Start () {
        m_Scoreboard = gameObject.GetComponent<UnityEngine.UI.Text>();
        m_CurrentScore = 0;
        UpdateScoreboard();
	}
	
    void UpdateScoreboard() {
        m_Scoreboard.text = m_CurrentScore.ToString("D14");
    }


    public void IncrementScore(long points) {
        m_CurrentScore += points;
        UpdateScoreboard();
    }


	// Update is called once per frame
	//void Update () {
		
	//}
    
}
