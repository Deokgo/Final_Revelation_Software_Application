using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disaPAPER : MonoBehaviour
{
    // Start is called before the first frame update
    public Interactable interactable;
    public float interactionRange = 1.0f; // Set the range as needed
    public KeyCode interactKey = KeyCode.E;
    //private GameObject paper;

    void Start()
    {
        //player = GameObject.FindWithTag("Player"); // Ensure your player is tagged as "Player"
    }

    void Update()
    {

    }

    public void Disappear()
    {
        gameObject.SetActive(false);
    }
}
