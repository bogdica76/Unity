using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour {
    public static DialogueManager Instance { get; set; }
    public GameObject dialoguePanel;

    public string npc_name;

    public List<string> dialogueLines = new List<string>();
    // Use this for initialization
    void Awake () {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
	}

    public void AddNewDialogue(string[] lines, string npcName)
    {
        dialogueLines = new List<string>(lines.Length);
        dialogueLines.AddRange(lines);
        npc_name = npcName;
    }
}
