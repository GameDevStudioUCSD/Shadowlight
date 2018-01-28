using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour {
    public UnityEvent OnOption1;
    public UnityEvent OnOption2;
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
        OnOption1.Invoke();
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
            
            OnOption2.Invoke();
            animator.Play("ToggleRight");
        }
        else
        {
            OnOption1.Invoke();
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
