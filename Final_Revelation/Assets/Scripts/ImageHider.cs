using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHider : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject imageToHide; // Assign this in the inspector

    // Call this method when the button is clicked
    public void HideImage()
    {
        imageToHide.SetActive(false); // This hides the image
    }
}
