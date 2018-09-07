using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour {
    public GameObject infoPanel;
    public Text title;
    public Text message;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ShowInfo(string aTitle, string aMessage) {
        infoPanel.SetActive(true);
        title.text = aTitle;
        message.text = aMessage;
        StartCoroutine(HideInfo());
    }

    IEnumerator HideInfo() {
        yield return new WaitForSeconds(2);
        infoPanel.SetActive(false);
    }
}
