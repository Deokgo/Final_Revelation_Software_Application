using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reaper2 : MonoBehaviour
{
    public Dialogue dialogue;

    public static Reaper2 Instance;

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
