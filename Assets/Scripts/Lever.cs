using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;
[RequireComponent(typeof(Animator))]

/**
 *  Class for controlling an interactable lever object. Has left and right
 *  states where either state is always toggled, and each one is associated
 *  with an event. On interaction, the current state changes to the other one.
 */
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
        // Should not be null because of [RequireComponent(typeof(Animator))]
        Assert.IsNotNull(animator, name + " requires an Animator component.");
    }

    private void OnEnable()
    {
        // Invoke the event associated with the default state of the lever
        if (left)
        {
            leftMode.Invoke();
        }
        else
        {
            rightMode.Invoke();
        }
    }

    private void Update()
    {
        // Player must be in range to interact with the lever
        // TODO: Change to interact button
        if (inRange == true && Input.GetKeyDown("q"))
        {
            Toggle();
        }
    }

    /**
     * Switches the current state to the other state and invokes the event
     * associated with that state.
     */
    void Toggle()
    {
        // If toggled left, switch to right
        if(left)
        {
            
            rightMode.Invoke();
            animator.Play("ToggleRight");
        }
        // If toggled right, switch to left
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
        // Check that only a player object can interact with the lever
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