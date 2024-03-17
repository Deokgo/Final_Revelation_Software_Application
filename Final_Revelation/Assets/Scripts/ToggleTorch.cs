using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTorch : MonoBehaviour
{
    public Animator animator;
    private bool isTorchOn = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Toggle()
    {
        isTorchOn = !isTorchOn;
        // Set the boolean parameter of the Animator
        animator.SetBool("on", isTorchOn);
        animator.SetBool("off", !isTorchOn);
    }
}
