using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject MyImage;
    public GameObject ImageHolder;
    private Sprite img1;

    // Call this method when the button is clicked
    public void HideImage()
    {
        MyImage = GameObject.FindWithTag("ImageFrame");
        ImageHolder = MyImage.transform.Find("ImageHolder").gameObject;
        ImageHolder.SetActive(false);
    }
}