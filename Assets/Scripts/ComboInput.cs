using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Works with the ComboUnlockable script to allow "unlockable" objects to
 * perform an action as a result of the simultaneous activation of multiple 
 * "input" objects. Attach this script to all "input" objects that are needed
 * to trigger the event.
 */
public class ComboInput : MonoBehaviour
{
    [SerializeField]
    private int value;
    [SerializeField]
    private GameObject[] objects;
    // Each object sends a value to the "unlockable" object, which will perform
    // an action when its total value reaches over its assigned threshold
    // Send a value to the assigned unlockable object
    public void sendValue(GameObject obj)
    {
        ComboUnlockable script = obj.GetComponent<ComboUnlockable>();
        if (script != null)
        {
            script.Add(value);
        }
    }
    // Send a value to all assigned unlockable objects
    public void sendValueToAll()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            ComboUnlockable script = objects[i].GetComponent<ComboUnlockable>();
            if (script != null)
            {
                script.Add(value);
            }
        }
    }
    
    // Remove value from the assigned unlockable object
    public void removeValue(GameObject obj)
    {
        ComboUnlockable script = obj.GetComponent<ComboUnlockable>();
        if (script != null)
        {
            script.Subtract(value);        
        }
    }
    
    // Remove value from all assigned unlockable objects
    public void removeValueToAll()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            ComboUnlockable script = objects[i].GetComponent<ComboUnlockable>();
            if (script != null)
            {
                script.Subtract(value);
            }
        }
    }
}
