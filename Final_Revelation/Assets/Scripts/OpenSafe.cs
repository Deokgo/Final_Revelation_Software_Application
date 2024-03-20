using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;
using System;

public class OpenSafe : MonoBehaviour
{
    public GameObject UserInput, Input;

    // Start is called before the first frame update
    void Start()
    {
        UserInput = GameObject.FindWithTag("UserInput");
        Input = GameObject.FindWithTag("Input");
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SafePrompt()
    {
        Input.GetComponent<InputField>().text = null;
        UserInput.GetComponent<Canvas>().enabled = true;
    }
}
