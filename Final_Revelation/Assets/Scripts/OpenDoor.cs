using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI paperText;
    public TextMeshProUGUI keyText;
    public int paperCollected = 0;    // Number of papers collected
    public int keyCollected = 0;
    private Sprite img1;
    public GameObject MyImage;
    public GameObject ImageHolder;
    // public bool isTorch1Lit = true;
    // public bool isTorch2Lit = false;
    // public bool isTorch3Lit = true;
    // public bool isTorch4Lit = false;
    // public SpriteRenderer torch1;
    // public SpriteRenderer torch2;
    // public SpriteRenderer torch3;
    // public SpriteRenderer torch4;
    public ToggleTorch torch1;
    public ToggleTorch torch2;
    public ToggleTorch torch3;
    public ToggleTorch torch4;

    // 1st progress data for 
    private string playerUsername = "";
    private int nextLvl = 2;
    private float player_position_x = -0.08565235f;
    private float player_position_y = 0.01465917f;
    private int remainingHealth = 3;
    public GameObject FinalDoor;


    void Start()
    {
        //paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        MyImage = GameObject.FindWithTag("ImageFrame");
        ImageHolder = MyImage.transform.Find("ImageHolder").gameObject;
        GameObject torch1GameObject = GameObject.FindWithTag("Torch1");
        GameObject torch2GameObject = GameObject.FindWithTag("Torch2");
        GameObject torch3GameObject = GameObject.FindWithTag("Torch3");
        GameObject torch4GameObject = GameObject.FindWithTag("Torch4");

        if (torch1GameObject != null)
            torch1 = torch1GameObject.GetComponent<ToggleTorch>();
        if (torch2GameObject != null)
            torch2 = torch2GameObject.GetComponent<ToggleTorch>();
        if (torch3GameObject != null)
            torch3 = torch3GameObject.GetComponent<ToggleTorch>();
        if (torch4GameObject != null)
            torch4 = torch4GameObject.GetComponent<ToggleTorch>();

        if (torch1 == null || torch2 == null || torch3 == null || torch4 == null)
        {
            //Debug.Log("One of the torch references is not set.");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Unlock()
    {

        if (!torch1.isTorchOn && torch2.isTorchOn && !torch3.isTorchOn && torch4.isTorchOn) // if the torches are in the correct order, it should open
        {
            playerUsername = Menu_Script.userInput;
            StartCoroutine(insertProgressPlayer("http://localhost/unity2/progressInsert.php", playerUsername, nextLvl, player_position_x, player_position_y, paperCollected, keyCollected, remainingHealth));
            Debug.Log("CONGRATULATIONS");
            SceneManager.LoadScene("Level2");
        }
        else
        {
            Image imageComponent = ImageHolder.GetComponent<Image>();
            img1 = Resources.Load<Sprite>("Images/ah sarado"); // Replace with the actual path and sprite name without the extension
            imageComponent.sprite = img1; // Assign the loaded sprite to the image component
            ImageHolder.SetActive(true);
            Debug.Log("THE DOOR IS LOCKED");
        }
    }
    public void UnlockFinalDoor()
    {
        if (keyText.text == "1/1")
        {
            FinalDoor = GameObject.FindWithTag("FinalDoor");
            FinalDoor.SetActive(false);
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
