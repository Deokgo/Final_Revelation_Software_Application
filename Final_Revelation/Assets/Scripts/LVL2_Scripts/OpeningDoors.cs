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
    public GameObject Jumpscare, ImageJumpscare, UserInput;
    //public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init()
    {
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        Jumpscare = GameObject.FindWithTag("Jumpscare");
        ImageJumpscare = Jumpscare.transform.Find("ImageJumpscare").gameObject;
        UserInput = GameObject.FindWithTag("UserInput");
    }

    public void FakeDoor1()
    {
        if (paperText.text == "5/5")
        {
            Image imageComponent = ImageJumpscare.GetComponent<Image>();
            img1 = Resources.Load<Sprite>("Jumpscare/Jumpscare1");
            imageComponent.sprite = img1;
            ImageJumpscare.SetActive(true);
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
            //audioSource.Play();
            StartCoroutine(DisableImg());
        }
        else
        {
            Debug.Log("Find all the papers first.");
        }
    }

    public void MainDoor()
    {
        Debug.Log("Find all the papers first.");
        if (paperText.text == "5/5")
        {
            UserInput.GetComponent<Canvas>().enabled = true;
        }
    }

    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(2);
        ImageJumpscare.SetActive(false);
    }
}
