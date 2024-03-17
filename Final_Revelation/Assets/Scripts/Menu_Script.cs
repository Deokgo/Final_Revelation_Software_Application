using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public static string userInput;
    public static int currentlvl = 0;

    public void ResumeGame()
    {
        userInput = "deokgoo";
        StartCoroutine(getPlayerLevel("http://localhost/unity2/getPlayerLevel.php", userInput));
    }
    public void PlayGame()
    {
        StartCoroutine(storePlayer("http://localhost/unity2/playerInsert.php", userInput));
    }

    public void GotoPlayMenu()
    {
        SceneManager.LoadScene("PlayMenu");
    }
    public void GotoMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GotoStart()
    {
        SceneManager.LoadScene("StartingMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void ReadInput(string username)
    {
        userInput = username;
        Debug.Log(userInput);
    }
    IEnumerator getPlayerLevel(string url, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);

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

        currentlvl = int.Parse(uwr.downloadHandler.text);

        if (currentlvl != 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("No Player Information Found!");
        }
    }
    IEnumerator storePlayer(string url, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);

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

            if (uwr.downloadHandler.text == "Player Saved!")
                StartCoroutine(storePlayerProgress("http://localhost/unity2/progressInsert.php", userInput));
        }
    }
    IEnumerator storePlayerProgress(string url, string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", "1");
        form.AddField("player_position_x", "-22.06852");
        form.AddField("player_position_y", "30.67065");
        form.AddField("paper_collected", "0");
        form.AddField("key_collected", "0");
        form.AddField("remaining_health", "3");

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

            if (uwr.downloadHandler.text == "Player Progress Saved!")
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
