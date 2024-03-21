using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public bool isInRange = false;
    public KeyCode interactKey;
    public UnityEvent interactAction;
    public GameObject exclamationPoint;

    public GameObject PlayerCanvas;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerCanvas = GameObject.FindWithTag("PlayerCanvas");
        exclamationPoint = PlayerCanvas.transform.Find("ExclamationMark").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(interactKey))
            {
                interactAction.Invoke();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = true;
            exclamationPoint.SetActive(true); // Show the exclamation point
            //lvl1Instruction.Instance.TriggerDialogue();
            //Debug.Log("Player is now in range");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInRange = false;
            exclamationPoint.SetActive(false); // Hide the exclamation point
            //DialogueManager.Instance.EndDialogue();
            //Debug.Log("Player is not in range");
        }
    }
}
