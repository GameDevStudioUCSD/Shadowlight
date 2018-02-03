using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.Assertions;

/**
 * Class for a door that can open and close.
 */
[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{
    public UnityEvent onOpen;
    public UnityEvent onClose;
    private Animator animator = null;
    private bool open = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Should not be null because of the RequireComponent attribute.
        Assert.IsNotNull(animator, name + " requires an Animator component.");
    }

    public void Open()
    {
        open = true;
        onOpen.Invoke();
    }

    public void Close()
    {
        open = false;
        onClose.Invoke();
    }

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