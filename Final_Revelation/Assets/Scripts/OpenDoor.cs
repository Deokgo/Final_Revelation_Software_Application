using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    void Start()
    {
        paperText = GameObject.FindWithTag("PaperText").GetComponent<TextMeshProUGUI>();
        keyText = GameObject.FindWithTag("KeyText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Unlock()
    {
        paperCollected = int.Parse(paperText.text.Split('/')[0]);
        keyCollected = int.Parse(keyText.text.Split('/')[0]);
        if (paperCollected == 5 && keyCollected == 1)
        {
            Image imageComponent = MyImage.GetComponent<Image>(); // Make sure this is the Image component of the UI
            MyImage.SetActive(true); // Ensure the GameObject is active to display the image
            Debug.Log("BOOM CLUE! SHEESH~");
        }
        else
        {
            Debug.Log("KUNIN MO MUNA LAHAT!");
        }
    }
}
