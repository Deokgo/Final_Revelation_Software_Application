using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Final_Scene_Dialogue : MonoBehaviour
{
    public Dialogue dialogue;

    public static Final_Scene_Dialogue Instance;

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
