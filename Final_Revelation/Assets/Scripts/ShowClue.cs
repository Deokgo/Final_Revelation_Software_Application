using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Data;
using TMPro;

public class ShowClue : MonoBehaviour
{
    public TextMeshProUGUI paperText;
    public GameObject MyImage;
    public GameObject ImageHolder, UserInput;
    public Sprite img1;
    // Start is called before the first frame update
    void Start()
    {
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        MyImage = GameObject.FindWithTag("ImageFrame");
        ImageHolder = MyImage.transform.Find("ImageHolder").gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Show()
    {
        Debug.Log("NAGSHOW SHEESH");
        if (paperText.text == "5/5")
        {
            Image imageComponent = ImageHolder.GetComponent<Image>();
            imageComponent.sprite = img1; // Assign the loaded sprite to the image component
            ImageHolder.SetActive(true);
        }
    }
}
