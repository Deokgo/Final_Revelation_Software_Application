using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;

public class Movement : MonoBehaviour
{

    public Animator animator;
    public SpriteRenderer sprite;
    public float moveSpeed = 2.0f;
    public float speedLimiter = 0.7f;
    public Rigidbody2D rb;
    public GameObject go;
    protected bool idle = false, run = false, grab_item = false, dead = false;
    public string input;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        Run();
        Idle();
        Grab_Item();
        Dead();

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||  Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
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
    
    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        //StartCoroutine(retrieveCollectedItems("http://localhost/unity/FetchData.php"));
    }

    IEnumerator storeCollectedItem(string url, string item, string pow)
    {
        WWWForm form = new WWWForm();
        form.AddField("item", item);
        form.AddField("power", pow);

        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            text1.text=uwr.downloadHandler.text;
        }
    }


    IEnumerator retrieveCollectedItems(string url)
    {
        WWWForm form = new WWWForm();
        UnityWebRequest uwr = UnityWebRequest.Post(url, form);
        yield return uwr.SendWebRequest();

        if (uwr.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
            text1.text = uwr.downloadHandler.text;
        }
    }
    */
}
