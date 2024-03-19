using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introNewGame : MonoBehaviour
{
    public Dialogue dialogue;

    public static introNewGame Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
