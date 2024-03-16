using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    //public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public static DialogueTrigger Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("Dialogue triggered");
            TriggerDialogue();
        }
    }
}
