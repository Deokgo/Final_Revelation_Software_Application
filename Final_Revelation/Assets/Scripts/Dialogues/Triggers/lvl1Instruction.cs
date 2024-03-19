using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl1Instruction : MonoBehaviour
{
    public Dialogue dialogue;

    public static lvl1Instruction Instance;

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
