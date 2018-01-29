using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

[RequireComponent(typeof(Animator))]

public class Lever : MonoBehaviour {
    public UnityEvent leftMode;
    public UnityEvent rightMode;
    private bool left;
    private Animator animator;
    private bool inRange;

    private void Start()
    {
        left = true;
        inRange = false;
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator);
    }

    private void OnEnable()
    {
        if (left)
        {
            leftMode.Invoke();
        }
        else
        {
            rightMode.Invoke();
        }
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
            
            rightMode.Invoke();
            animator.Play("ToggleRight");
        }
        else
        {
            leftMode.Invoke();
            animator.Play("ToggleLeft");
        }
        left = !left;
    }

    // Because OnTriggerStay2D was only being called when collider was moving
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
        }
    }
}
