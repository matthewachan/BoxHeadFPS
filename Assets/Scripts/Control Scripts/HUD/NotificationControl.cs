using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationControl : MonoBehaviour {
    private UnityEngine.UI.Text m_NotificationText;
	
    
    // Use this for initialization
	void Start () {
        m_NotificationText = gameObject.GetComponent<UnityEngine.UI.Text>();

	}

    public void PostNotification(string text) {
        StartCoroutine(TimeoutNotification(text, 2));
    }

    IEnumerator TimeoutNotification(string text, float time) {
        m_NotificationText.text = text;
        yield return new WaitForSeconds(time);
        m_NotificationText.text = "";
    }
	
	// Update is called once per frame
	//void Update () {
		
	//}
}
