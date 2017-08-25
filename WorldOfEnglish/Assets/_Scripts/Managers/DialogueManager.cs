using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	public static DialogueManager Instance { get; set;}

	public GameObject dialoguePanel;

	public string npcName;

	public List<string> dialogueLines = new List<string>();

	Button okButton;
	Text dialogueText, nameText;
	int dialogueIndex;
	// Use this for initialization
	void Awake () {
		okButton = dialoguePanel.transform.Find ("Ok").GetComponent<Button> ();
		dialogueText = dialoguePanel.transform.Find ("Text").GetComponent<Text> ();
		nameText = dialoguePanel.transform.Find ("Name").GetChild (0).GetComponent<Text> ();

		dialoguePanel.SetActive (false);

		okButton.onClick.AddListener (delegate {
			ContinueDialogue ();
		});

		if (Instance != null && Instance != this) {
			Destroy (gameObject);
		} else {
			Instance = this;
		}
	}
	
	public void AddNewDialogue(string[] lines, string npcName){
		dialogueIndex = 0;
		dialogueLines = new List<string>(lines.Length);
		foreach (string line in lines) {
			dialogueLines.Add (line);
		}
		this.npcName = npcName;
		CreateDialogue ();
	}

	public void CreateDialogue(){
		dialogueText.text = dialogueLines [dialogueIndex];
		nameText.text = npcName;
		dialoguePanel.SetActive (true);
	}

	public void ContinueDialogue(){
		if (dialogueIndex < dialogueLines.Count - 1) {
			dialogueIndex++;
			dialogueText.text = dialogueLines [dialogueIndex];
		} else {
			dialoguePanel.SetActive (false);
		}
	}
}
