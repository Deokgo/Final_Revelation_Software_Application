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
    public GameObject go, UserInput, Input;
    public Animator safeAnimator;
    private string playerUsername = Menu_Script.userInput;
    public TextMeshProUGUI keyText;

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.FindWithTag("Player");
        safeAnimator = GameObject.FindWithTag("Safe").GetComponent<Animator>();
        UserInput = GameObject.FindWithTag("UserInput");
        Input = GameObject.FindWithTag("Input");
        if (keyText.text == "1/1")
        {
            safeAnimator.SetBool("closed", false);
            safeAnimator.SetBool("opened", false);
            safeAnimator.SetBool("empty", true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void SafePrompt()
    {
        if (safeAnimator.GetBool("closed"))
        {
            Input.GetComponent<InputField>().text = null;
            UserInput.GetComponent<Canvas>().enabled = true;
        }
        else if (safeAnimator.GetBool("opened"))
        {
            StartCoroutine(storeCollectedItem("http://localhost/unity2/gameElementAdd.php", playerUsername, 3, "Key"));
            StartCoroutine(updatePlayer("http://localhost/unity2/progressUpdate2.php", playerUsername, 3, go.transform.position.x, go.transform.position.y, 1));
            safeAnimator.SetBool("closed", false);
            safeAnimator.SetBool("opened", false);
            safeAnimator.SetBool("empty", true);
            keyText.text = "1/1";
        }
        else if (safeAnimator.GetBool("closed"))
        {
            //do nothing
        }

    }
    IEnumerator storeCollectedItem(string url, string username, int lvl, string gameElementTag)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", lvl);
        form.AddField("game_element_collected", gameElementTag);
        Debug.Log(gameElementTag);
        using (UnityWebRequest uwr = UnityWebRequest.Post(url, form))
        {
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
    IEnumerator updatePlayer(string url, string username, int lvl, double player_position_x, double player_position_y, int keyCollected)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", lvl);
        form.AddField("player_position_x", player_position_x.ToString());
        form.AddField("player_position_y", player_position_y.ToString());
        form.AddField("key_collected", keyCollected);

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
