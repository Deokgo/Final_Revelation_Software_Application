using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;
using System;

public class GetUserInput : MonoBehaviour
{
    [SerializeField] InputField inputField;
    [SerializeField] Text resultText;

    // for the next level (3)
    private string playerUsername = Menu_Script.userInput;
    public int paperCollected = 0;    // Number of papers collected
    public int keyCollected = 0;
    private int nextLvl = 3;
    private double player_position_x = -0.08565235;
    private double player_position_y = 0.01465917;
    private int remainingHealth = 3;
    public Animator animator;
    public GameObject UserInput;
    public AudioSource wrongSFX;

    void Start()
    {
        UserInput = GameObject.FindWithTag("UserInput");
    }
    public void ValidateInput()
    {
        string input = inputField.text;

        if (input.ToLower() == "andrew")
        {
            StartCoroutine(insertProgressPlayer("http://localhost/unity2/progressInsert.php", playerUsername, nextLvl, player_position_x, player_position_y, paperCollected, keyCollected, remainingHealth));
            resultText.text = "Correct!";
            Debug.Log("CONGRATULATIONS");
            SceneManager.LoadScene("Level3");
        }
        else
        {
            wrongSFX.Play();
            resultText.text = "Wrong!";
        }
    }
    public void CheckCode()
    {
        string input = inputField.text;
        animator = GameObject.FindWithTag("Safe").GetComponent<Animator>();
        if (input == "14713") //do this if closed is true
        {
            animator.SetBool("closed", false);
            animator.SetBool("opened", true);
            animator.SetBool("empty", false);
            resultText.text = "Correct!";
            Debug.Log("CONGRATULATIONS");
            UserInput.GetComponent<Canvas>().enabled = false;

            SafeTrigger.Instance.TriggerDialogue();
        }
        else
        {
            resultText.text = "Wrong!";
        }
    }

    IEnumerator insertProgressPlayer(string url, string username, int nextLvl, double player_position_x, double player_position_y, int paperCollected, int keyCollected, int remainingHealth)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", nextLvl);
        form.AddField("player_position_x", player_position_x.ToString());
        form.AddField("player_position_y", player_position_y.ToString());
        form.AddField("paper_collected", paperCollected);
        form.AddField("key_collected", keyCollected);
        form.AddField("remaining_health", remainingHealth);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }
}
