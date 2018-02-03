using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using System.Collections;

/**
 * Class for a door that can open and close.
 */
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    private Animator animator = null;
    private bool open = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Should not be null because of the RequireComponent attribute.
        Assert.IsNotNull(animator, name + " requires an Animator component.");
    }

    /**
     * Opens door.
     */
    public void Open()
    {
        // Door can only open if it is already closed
        if (!open)
        {
            open = true;
            animator.Play("Open");
        }
    }


    /**
     * Closes door.
     */
    public void Close()
    {
        // Door can only close if it is already open
        if (open)
        {
            open = false;
            animator.Play("Close");
        }
    }

    /**
     * Opens door if closed, closes door if opened.
     */
    public void Switch()
    {
        if (open)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
