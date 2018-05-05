using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Assertions;

/**
 *  Class for controlling an interactable lever object. Has left and right
 *  states where either state is always toggled, and each one is associated
 *  with an event. On interaction, the current state changes to the other one.
 */
[RequireComponent(typeof(Animator))]
public class Lever : MonoBehaviour {
    public UnityEvent leftMode = null;
    public UnityEvent rightMode = null;
    private Animator animator = null;
    private bool left = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        // Should not be null because of the RequireComponent attribute.
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

    /**
     * Switches the current state to the other state and invokes the event
     * associated with that state.
     */
    public void Toggle()
    {
        // If toggled left, switch to right
        if (left)
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

    public void HighlightTargets() 
    {
        if (left)
        {
            FindAllTargets(rightMode);
        }
        else 
        {
            FindAllTargets(leftMode);
        }
    }

    void FindAllTargets(UnityEvent unityevent)
    {
        // Draws a line to all targets that the interatable object will affect
        for (int i = 0; i < unityevent.GetPersistentEventCount(); i++)
        {
            GameObject target = ((Component)unityevent.GetPersistentTarget(i)).gameObject;
            Debug.Log(unityevent.GetPersistentTarget(0));    // Object is not null
            Debug.Log(target);  // GameObject is null
            DrawLine(transform.position, target.transform.position, Color.blue, 5f);

        }
    }

    void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
    {
        GameObject myLine = new GameObject();
        myLine.transform.position = start;
        myLine.AddComponent<LineRenderer>();
        LineRenderer lr = myLine.GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
        lr.startColor = color;
        lr.startWidth = 0.1f;
        lr.SetPosition(0, start);
        lr.SetPosition(1, end);
        lr.sortingOrder = 999;
        Destroy(myLine, duration);
    }
}
