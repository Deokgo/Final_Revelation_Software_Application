using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton1 : MonoBehaviour
{
    public Dialogue dialogue;

    public static Skeleton1 Instance;

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
