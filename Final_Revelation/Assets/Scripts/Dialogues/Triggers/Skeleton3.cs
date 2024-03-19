using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton3 : MonoBehaviour
{
    public Dialogue dialogue;

    public static Skeleton3 Instance;

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
