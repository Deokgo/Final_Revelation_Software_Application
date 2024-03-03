using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;

public class Movement : MonoBehaviour
{

    public Animator animator;
    public SpriteRenderer sprite;
    public float moveSpeed = 2.0f;
    public float speedLimiter = 0.7f;
    public Rigidbody2D rb;
    public GameObject go;
    public TextMeshProUGUI paperText;
    public TextMeshProUGUI keyText;
    protected bool idle = false, run = false, grab_item = false, dead = false;
    public string input;

    public int playerId = 1;
    public int paperCollected = 0;    // Number of papers collected
    public int keyCollected = 0;      // Number of keys collected
    public int remainingHealth = 3; // The player's remaining health

    // Start is called before the first frame update
    void Start()
    {
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        keyText = GameObject.FindWithTag("KeyText").GetComponent<TextMeshProUGUI>();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Idle();
        Grab_Item();
        Dead();

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

        StartCoroutine(playerPosition("http://localhost/unity/playerFetch.php", playerId));
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

        }

    }
    ////////////////////
    //DATABASE CODES...
    ////////////////////


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ghost"))
        {
            paperCollected = int.Parse(paperText.text.Split('/')[0]);
            keyCollected = int.Parse(keyText.text.Split('/')[0]);
            remainingHealth -= 1;
            StartCoroutine(updatePlayer("http://localhost/unity/playerSaveUpdate.php", playerId, go.transform.position.x, go.transform.position.y, paperCollected, keyCollected, remainingHealth));
        }
    }

    IEnumerator updatePlayer(string url, int playerId, double player_position_x, double player_position_y, int paperCollected, int keyCollected, int remainingHealth)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_id", playerId);
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
    IEnumerator playerPosition(string url, int playerId)
    {
        WWWForm form = new WWWForm();
        form.AddField("player_id", playerId);

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

        //change the current player position
        float x, y;
        string[] coordinates = (uwr.downloadHandler.text).Split('/');
        x = float.Parse(coordinates[0]);
        y = float.Parse(coordinates[1]);

        Debug.Log("Received: " + coordinates[0]);
        Debug.Log("Received: " + coordinates[1]);

        go.transform.position = new Vector3(x,y,10);
    }
}
