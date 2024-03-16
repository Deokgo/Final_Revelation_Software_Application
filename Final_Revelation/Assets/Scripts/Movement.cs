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

    public Animator animator;
    public SpriteRenderer sprite;
    public float moveSpeed = 2.0f;
    public float speedLimiter = 0.7f;
    public Rigidbody2D rb;
    public GameObject go, lifeObject;
    public TextMeshProUGUI paperText;
    public TextMeshProUGUI keyText;
    protected bool idle = false, run = false, grab_item = false, dead = false;
    public string input;
    public string playerUsername = "deokgoo";
    public string[] life = { "Life3", "Life2", "Life1" };
    public float delay = 3;
    float timer;

    public int currentlvl;
    public int paperCollected = 0;    // Number of papers collected
    public int keyCollected = 0;      // Number of keys collected
    public int remainingHealth; // The player's remaining health

    // Audio (SFX when collided with the ghost)
    public AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        try
        {
            keyText = GameObject.FindWithTag("KeyText").GetComponent<TextMeshProUGUI>();
        }
        catch (Exception ex)
        {
            //
        }

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
    }
    void RespawnPlayer()
    {
        // reset life (spawn charter)
        remainingHealth = 3;
        go.transform.position = new Vector3(-22.06852f, 30.67065f, 10f);

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
    public void Init()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        go = GameObject.FindWithTag("Player");
        rb = go.GetComponent<Rigidbody2D>();

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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ghost"))
        {
            // SFX
            audioPlayer.Play();

            paperCollected = int.Parse(paperText.text.Split('/')[0]);
            keyCollected = int.Parse(keyText.text.Split('/')[0]);
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
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }

        currentlvl = int.Parse(uwr.downloadHandler.text);

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
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }

        //Set player's progress

        float x, y;
        string[] player = (uwr.downloadHandler.text).Split('/');
        x = float.Parse(player[0]);
        y = float.Parse(player[1]);
        Debug.Log("Paper mo ay:" + player[2]);
        paperText.text = player[2] + "/5";
        //keyText.text = player[3] + "/1";

        paperCollected = int.Parse(player[2]);
        // keyCollected = int.Parse(player[3]);
        remainingHealth = int.Parse(player[4]);
        SetLives();

        if (remainingHealth == 0)
        {
            RespawnPlayer();
        }

        go.transform.position = new Vector3(x, y, 10);

    }
}
