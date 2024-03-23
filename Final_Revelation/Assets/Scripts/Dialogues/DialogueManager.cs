using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    //public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.05f;

    public Animator animator;
    public GameObject GrimReaper, Killer, Wife, Player;
    public int lineCounter;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        lines = new Queue<DialogueLine>();
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

        lineCounter = lines.Count;
        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }
        
        if (lineCounter == 22)
        {
            GrimReaper = GameObject.FindWithTag("Reaper2");
            Killer = GameObject.FindWithTag("killer");
            Wife = GameObject.FindWithTag("wife");
            Player = GameObject.FindWithTag("Player");

            if (lines.Count == 15)
            {
                GrimReaper.transform.position = new Vector3(58, -64, 0);
            }
            if (lines.Count == 11)  
            {
                GrimReaper.SetActive(false);
                Killer.SetActive(false);

                Wife.transform.position = new Vector3(60, -65, 0);
            }
            if (lines.Count == 3)
            {
                Player.SetActive(false);
            }
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
