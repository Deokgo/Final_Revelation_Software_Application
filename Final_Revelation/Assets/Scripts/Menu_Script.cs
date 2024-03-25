using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour
{
    public static string userInput;
    public static int currentlvl = 0;

    public void ResumeGame(string username)
    {
        userInput = username;
        StartCoroutine(searchUsername2("http://localhost/unity2/searchUsername.php", userInput));
    }
    public void PlayGame()
    {
        StartCoroutine(truncatePlayerProgress("http://localhost/unity2/truncateProgress.php", userInput));
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
    public void ReadInput()
    {
        //userInput = username; //may times na dumodoble ung input sa database dahil dito I think...
        StartCoroutine(searchUsername("http://localhost/unity2/searchUsername.php", userInput));
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
            switch (currentlvl)
            {
                case 1: SceneManager.LoadScene("Level1"); break;
                case 2: SceneManager.LoadScene("Level2"); break;
                case 3: SceneManager.LoadScene("Level3"); break;
            }
        }
        else
        {
            Debug.Log("No Player Information Found!");
        }
    }
    IEnumerator truncatePlayerProgress(string url, string username)
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

            if (uwr.downloadHandler.text == "Player Progress Deleted!")
                StartCoroutine(truncateGameElement("http://localhost/unity2/truncateGameElement.php", userInput));
        }
    }
    IEnumerator truncateGameElement(string url, string username)
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

            if (uwr.downloadHandler.text == "Player Progress Deleted!")
                StartCoroutine(storePlayerProgress("http://localhost/unity2/progressInsert.php", userInput, 1));
        }
    }
    IEnumerator storePlayerProgress(string url, string username, int lvl)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", lvl);
        form.AddField("player_position_x", "-0.08565235");
        form.AddField("player_position_y", "0.01465917");
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
                StartCoroutine(getPlayerLevel("http://localhost/unity2/getPlayerLevel.php", userInput));
        }
    }
    IEnumerator restartGameElement(string url, string username, int lvl)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", lvl);

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

            if (uwr.downloadHandler.text == "Level Game Elements Deleted!")
                StartCoroutine(restartLevelProgress("http://localhost/unity2/restartProgress.php", username, lvl));
        }
    }
    IEnumerator restartLevelProgress(string url, string username, int lvl)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", lvl);

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

            if (uwr.downloadHandler.text == "Level Progress Restarted!")
                StartCoroutine(storePlayerProgress("http://localhost/unity2/progressInsert.php", username, lvl));
        }
    }
    IEnumerator searchUsername(string url, string username)
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
                Debug.Log("1Received: " + uwr.downloadHandler.text);
            }
            if (uwr.downloadHandler.text == "Username not found.")
                StartCoroutine(insertPlayer("http://localhost/unity2/insertUsername.php", username));
            else
            {

                //Debug.Log("Username already exists.");
            }
        }
    }
    IEnumerator searchUsername2(string url, string username)
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
                Debug.Log("2Received: " + uwr.downloadHandler.text);
            }
            if (uwr.downloadHandler.text == "Username exists.")
                StartCoroutine(getPlayerLevel("http://localhost/unity2/getPlayerLevel.php", userInput));
            else
            {
                //Debug.Log("Username already exists.");
            }
        }
    }
    IEnumerator insertPlayer(string url, string username)
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
                if (uwr.downloadHandler.text == "Username inserted successfully.")
                    StartCoroutine(storePlayerProgress("http://localhost/unity2/progressInsert.php", username, 1));
                SceneManager.LoadScene("Level1");
            }
        }
    }
}
