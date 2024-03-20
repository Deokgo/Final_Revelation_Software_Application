using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;
using System;

public class OpeningDoors : MonoBehaviour
{
    public TextMeshProUGUI paperText;
    private Sprite img1;
    public GameObject Jumpscare, ImageJumpscare, UserInput, Input;
    public AudioSource audioSource;
    public GameObject GrimReaper;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (paperText.text == "5/5")
        {
            GrimReaper.transform.position = new Vector3(-58, 14, 10);
        }
    }

    public void Init()
    {
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        Jumpscare = GameObject.FindWithTag("Jumpscare");
        ImageJumpscare = Jumpscare.transform.Find("ImageJumpscare").gameObject;
        UserInput = GameObject.FindWithTag("UserInput");
        Input = GameObject.FindWithTag("Input");
        GrimReaper = GameObject.FindWithTag("Reaper");
    }

    public void FakeDoor1()
    {
        if (paperText.text == "5/5")
        {
            Image imageComponent = ImageJumpscare.GetComponent<Image>();
            img1 = Resources.Load<Sprite>("Jumpscare/Jumpscare1");
            imageComponent.sprite = img1;
            ImageJumpscare.SetActive(true);
            audioSource.Play();
            StartCoroutine(DisableImg());
        }
        else
        {
            Debug.Log("Find all the papers first.");
        }
    }

    public void FakeDoor2()
    {
        if (paperText.text == "5/5")
        {
            Image imageComponent = ImageJumpscare.GetComponent<Image>();
            img1 = Resources.Load<Sprite>("Jumpscare/Jumpscare2");
            imageComponent.sprite = img1;
            ImageJumpscare.SetActive(true);
            audioSource.Play();
            StartCoroutine(DisableImg());
        }
        else
        {
            Debug.Log("Find all the papers first.");
        }
    }

    public void MainDoor()
    {
        if(UserInput.GetComponent<Canvas>().enabled == false)
        {
            if (paperText.text == "5/5")
            {
                Input.GetComponent<InputField>().text = null;
                UserInput.GetComponent<Canvas>().enabled = true;
            }
            else
            {
                Debug.Log("Find all the papers first.");
            }
        }
        
    }

    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(2);
        ImageJumpscare.SetActive(false);
    }
}
