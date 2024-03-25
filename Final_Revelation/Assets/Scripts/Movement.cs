using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;
using System;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    public Animator animator;
    public SpriteRenderer sprite;
    public float moveSpeed = 4.0f;
    public float speedLimiter = 0.7f;
    public Rigidbody2D rb;
    public GameObject go, lifeObject, UserInput;
    public TextMeshProUGUI paperText, keyText;
    protected bool idle = false, run = false, grab_item = false, dead = false;
    public string input;
    public string playerUsername = "";
    public string[] life = { "Life3", "Life2", "Life1" };
    public float delay = 3;
    float timer;

    public int currentlvl = 0;
    public int paperCollected = 0;    // Number of papers collected
    public int keyCollected = 0;      // Number of keys collected
    public int remainingHealth; // The player's remaining health

    // Audio (SFX when collided with the ghost)
    public AudioSource audioPlayer;

    // Start is called before the first frame update
    void Awake()
    {
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        Init();
    }
    void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        Run();
        Idle();
        Grab_Item();
        Dead();

        if (remainingHealth == 0)
        {
            idle = false;
            run = false;
            grab_item = false;
            dead = true;

            SetBoolValue();
        }
        else
        {
            if ((DialogueManager.Instance.isDialogueActive == false) && (UserInput.GetComponent<Canvas>().enabled == false))
            {
                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    idle = false;
                    run = true;
                    grab_item = false;
                    dead = false;

                    SetBoolValue();

                    var input = Input.inputString;

                    if (input.Equals("a"))
                    {
                        sprite.flipX = true;
                    }
                    else if (input.Equals("d"))
                    {
                        sprite.flipX = false;
                    }
                }

                else if (Input.GetKey(KeyCode.E))
                {
                    idle = false;
                    run = false;
                    grab_item = true;
                    dead = false;

                    SetBoolValue();
                }

                else
                {
                    idle = true;
                    run = false;
                    grab_item = false;
                    dead = false;

                    SetBoolValue();
                }
            }
            else
            {
                idle = true;
                run = false;
                grab_item = false;
                dead = false;

                SetBoolValue();
            }
        }
    }
    void RespawnPlayer()
    {
        // reset life (spawn charter)
        remainingHealth = 3;
        go.transform.position = new Vector3(-0.08565235f, 0.01465917f, 10f);

        /////////////////////////
        //Update UI lives here...
        ////////////////////////

        foreach (string buhay in life)
        {
            Debug.Log("Checking tag: " + buhay);
            lifeObject = GameObject.FindGameObjectWithTag(buhay);
            lifeObject.GetComponent<SpriteRenderer>().enabled = true;
        }
        StartCoroutine(updatePlayer("http://localhost/unity2/progressUpdate.php", playerUsername, currentlvl, go.transform.position.x, go.transform.position.y, paperCollected, keyCollected, remainingHealth));
    }

    void SetLives()
    {
        for (int i = 1; i < remainingHealth; i++)
        {
            lifeObject = GameObject.FindGameObjectWithTag(life[i]);
            lifeObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    void SetBoolValue()
    {
        animator.SetBool("Idle", idle);
        animator.SetBool("Run", run);
        animator.SetBool("Grab_Item", grab_item);
        animator.SetBool("Dead", dead);
    }

    public void Dialogue()
    {
        introNewGame.Instance.TriggerDialogue();
    }

    public void Init()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        go = GameObject.FindWithTag("Player");
        rb = go.GetComponent<Rigidbody2D>();
        UserInput = GameObject.FindWithTag("UserInput");
        playerUsername = Menu_Script.userInput;
        for (int i = 1; i < 3; i++)
        {
            lifeObject = GameObject.FindGameObjectWithTag(life[i]);
            lifeObject.GetComponent<SpriteRenderer>().enabled = false;
        }

        StartCoroutine(getPlayerLevel("http://localhost/unity2/getPlayerLevel.php", playerUsername));
    }

    void Run()
    {
        if (run)
        {
            go.transform.Translate(Vector2.right * Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
            go.transform.Translate(Vector2.up * Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        }
    }

    void Idle()
    {
        if (idle)
        {

        }

    }

    void Grab_Item()
    {
        if (grab_item)
        {

        }

    }

    void Dead()
    {
        if (dead)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                RespawnPlayer();
            }
        }
    }
    public void SaveProgress()
    {
        Vector3 currentPosition = transform.position;
        float xPosition = currentPosition.x;
        float yPosition = currentPosition.y;
        StartCoroutine(updatePlayer("http://localhost/unity2/progressUpdate.php", playerUsername, currentlvl, xPosition, yPosition, paperCollected, keyCollected, remainingHealth));
    }
    public void RestartLevelProgress()
    {
        StartCoroutine(restartGameElement("http://localhost/unity2/restartGameElement.php", playerUsername, currentlvl));
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ghost"))
        {
            // SFX
            audioPlayer.Play();

            paperCollected = int.Parse(paperText.text.Split('/')[0]);
            if (keyText != null)
            {
                keyCollected = int.Parse(keyText.text.Split('/')[0]);
            }
            remainingHealth -= 1;
            try
            {
                // UI update
                lifeObject = GameObject.FindGameObjectWithTag(life[remainingHealth]);
                lifeObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            catch (IndexOutOfRangeException)
            {
                RespawnPlayer();
            }

            StartCoroutine(updatePlayer("http://localhost/unity2/progressUpdate.php", playerUsername, currentlvl, go.transform.position.x, go.transform.position.y, paperCollected, keyCollected, remainingHealth));
        }
    }

    IEnumerator updatePlayer(string url, string username, int lvl, double player_position_x, double player_position_y, int paperCollected, int keyCollected, int remainingHealth)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", lvl);
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
            //Debug.Log("Received: " + uwr.downloadHandler.text);
        }

        currentlvl = int.Parse(uwr.downloadHandler.text);
        Debug.Log("Current Level: " + currentlvl);

        StartCoroutine(getPlayerProgress("http://localhost/unity2/progressFetch.php", playerUsername, currentlvl));
    }
    IEnumerator getPlayerProgress(string url, string username, int lvl)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_username", username);
        form.AddField("player_level", lvl);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            //Debug.Log("Received: " + uwr.downloadHandler.text);
        }

        //Set player's progress
        float x, y;
        string[] player = (uwr.downloadHandler.text).Split('/');
        x = float.Parse(player[0]);
        y = float.Parse(player[1]);
        Debug.Log("Paper mo ay:" + player[2]);
        paperText.text = player[2] + "/5";
        if (keyText != null && !string.IsNullOrEmpty(keyText.text))
        {
            Debug.Log("Hindi ako null: " + player[3]);
            keyText.text = player[3] + "/1";
            keyCollected = int.Parse(player[3]);
        }

        paperCollected = int.Parse(player[2]);

        remainingHealth = int.Parse(player[4]);
        SetLives();

        if (paperCollected == 0 && keyCollected == 0 && remainingHealth == 3)
        {
            Dialogue();
        }

        if (remainingHealth == 0)
        {
            RespawnPlayer();
        }

        go.transform.position = new Vector3(x, y, 10);
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
            {
                StartCoroutine(getPlayerLevel("http://localhost/unity2/getPlayerLevel.php", playerUsername));
            }
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
}
