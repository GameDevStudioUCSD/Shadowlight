using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour {
    public UnityEvent leftOption;
    public UnityEvent rightOption;
    private bool left;
    private Animator animator;
    private bool inRange;

    void Awake()
    {
        left = true;
        inRange = false;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        leftOption.Invoke();
    }

    private void FixedUpdate()
    {
        // TODO: Change to interact button
        if (inRange == true && Input.GetKeyDown("q"))
        {
            Toggle();
        }
    }

    void Toggle()
    {
        if(left)
        {
            
            rightOption.Invoke();
            animator.Play("ToggleRight");
        }
        else
        {
            leftOption.Invoke();
            animator.Play("ToggleLeft");
        }
        left = !left;
    }

    // Because OnTriggerStay2D was only being called when collider was moving
    void OnTriggerEnter2D(Collider2D other)
    {
        inRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        inRange = false;
    }
}
