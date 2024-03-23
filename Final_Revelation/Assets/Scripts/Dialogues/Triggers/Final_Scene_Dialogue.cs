using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Final_Scene_Dialogue : MonoBehaviour
{
    public Dialogue dialogue;

    public static Final_Scene_Dialogue Instance;

    //public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.05f;

    public Animator animator;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
    }

    public void TriggerDialogue()
    {
        StartDialogue(dialogue);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        isDialogueActive = true;

        animator.Play("show");

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        else if (lines.Count == 8)
        {
            //grim reaper appeared code...
        }
        else if (lines.Count == 12)
        {
            //grim reaper and player disappear code...
        }
        else if (lines.Count == 20)
        {
            //player disappear code...
        }

        DialogueLine currentLine = lines.Dequeue();

        //characterIcon.sprite = currentLine.character.icon;
        characterName.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void EndDialogue()
    {
        isDialogueActive = false;
        animator.Play("hide");
    }
}
