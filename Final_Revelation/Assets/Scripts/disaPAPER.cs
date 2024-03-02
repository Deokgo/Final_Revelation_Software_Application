using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;


public class disaPAPER : MonoBehaviour
{
    // Start is called before the first frame update
    public Interactable interactable;
    public float interactionRange = 1.0f; // Set the range as needed
    public KeyCode interactKey = KeyCode.E;
    public int playerId = 1;
    public TextMeshProUGUI paperText;
    public TextMeshProUGUI keyText;

    void Start()
    {
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        keyText = GameObject.FindWithTag("KeyText").GetComponent<TextMeshProUGUI>();
        if (paperText == null)
        {
            Debug.LogError("No GameObject with tag 'PaperText' found.");
        }
        if (keyText == null)
        {
            Debug.LogError("No GameObject with tag 'KeyText' found.");
        }
    }

    void Update()
    {

    }

    public void Disappear()
    {
        string gameElementTag = gameObject.tag; // Retrieve the tag of the paper
        if (gameElementTag == "Key")
        {
            keyText.text = "1/1";
        }
        else if (gameElementTag.Substring(0, 5) == "Paper")
        {
            int currentPapers = int.Parse(paperText.text.Split('/')[0]);
            paperText.text = (currentPapers + 1).ToString() + "/5";
        }
        StartCoroutine(storeCollectedItem("http://localhost/unity/gameElementAdd.php", playerId, gameElementTag));
        gameObject.SetActive(false);
    }

    IEnumerator storeCollectedItem(string url, int player_id, string gameElementTag)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_id", player_id);
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
}
