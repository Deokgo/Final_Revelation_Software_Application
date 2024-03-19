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
    }

    public void FakeChest()
    {
        if (paperText.text == "5/5")
        {
            Debug.Log("This Chest is fake.");
        }
        else
        {
            Debug.Log("Find all the papers first.");
        }
    }

    public void MainChest()
    {
        Debug.Log("Find all the papers first.");
        if (paperText.text == "5/5")
        {
            Image imageComponent = ImageHolder.GetComponent<Image>();
            img1 = Resources.Load<Sprite>("Clues/Clue2");
            imageComponent.sprite = img1;
            ImageHolder.SetActive(true);
        }
    }
}
