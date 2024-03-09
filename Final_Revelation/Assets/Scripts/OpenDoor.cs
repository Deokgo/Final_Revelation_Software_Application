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
            Image imageComponent = MyImage.GetComponent<Image>();
            img1 = Resources.Load<Sprite>("Clues/Clue_1"); // Replace with the actual path and sprite name without the extension
            imageComponent.sprite = img1; // Assign the loaded sprite to the image component
            MyImage.SetActive(true);
            Debug.Log("BOOM CLUE! SHEESH~");
        }
        else
        {
            Debug.Log("KUNIN MO MUNA LAHAT!");
        }
    }
}
