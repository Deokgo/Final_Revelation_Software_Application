using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;
using System;


public class CheckChest : MonoBehaviour
{
    public TextMeshProUGUI paperText;
    public GameObject chestOb, mainChestOb;
    private Sprite img1;
    public GameObject MyImage;
    public GameObject ImageHolder;
    public string playerUsername = "deokgoo";
    public GameObject Jumpscare, ImageJumpscare, UserInput;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init()
    {
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        MyImage = GameObject.FindWithTag("ImageFrame");
        ImageHolder = MyImage.transform.Find("ImageHolder").gameObject;
        Jumpscare = GameObject.FindWithTag("Jumpscare");
        ImageJumpscare = Jumpscare.transform.Find("ImageJumpscare").gameObject;
        UserInput = GameObject.FindWithTag("UserInput");
    }

    public void FakeChest()
    {
        Image imageComponent = ImageJumpscare.GetComponent<Image>();
        img1 = Resources.Load<Sprite>("Jumpscare/Jumpscare2");
        imageComponent.sprite = img1;
        ImageJumpscare.SetActive(true);
        audioSource.Play();
        StartCoroutine(DisableImg());
    }
    IEnumerator DisableImg()
    {
        yield return new WaitForSeconds(2);
        ImageJumpscare.SetActive(false);
    }
}
