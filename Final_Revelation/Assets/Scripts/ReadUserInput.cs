using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadUserInput : MonoBehaviour
{
    public string userInput;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadInput(string username)
    {
        userInput = username;
        Debug.Log(userInput);
    }
}
