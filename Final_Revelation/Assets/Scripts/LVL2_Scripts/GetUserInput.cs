using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetUserInput : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;

    public void ValidateInput()
    {
        string input = inputField.text;

        if(input.ToLower() == "andrew")
        {
            resultText.text = "Correct!";
        }
        else
        {
            resultText.text = "Wrong!";
        }
    }
}
