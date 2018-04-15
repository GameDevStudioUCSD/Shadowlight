using System.Collections;
using System.Collections.Generic;
using UnityEngine; using UnityEngine.Events;

/**
 * Works with the ComboInput script to allow "unlockable" objects to
 * perform an action as a result of the simultaneous activation of multiple 
 * "input" objects. Attach this script to all "unlockable" objects that need to
 * be triggered by multiple objects.
 */
public class ComboUnlockable : MonoBehaviour
{
    public UnityEvent onThresholdReached;
    public UnityEvent onThresholdLost;
    [SerializeField]
    private int threshold;
    
    // Value at which event will trigger
    [SerializeField]
    private int currentValue;
    private bool thresholdReached;
    
    private void Awake()
    {
        thresholdReached = false;
    }
    // Adds to value being checked against threshhold
    public void Add(int value)
    {
        currentValue += value;
        Check();
    }
    // Subtracts from value being checked against threshhold
    public void Subtract(int value)
    {
        currentValue -= value;
        Check();
    }
    // Compares value to threshhold to see if event is triggered
    public void Check()
    {
        if (thresholdReached == false && threshold == currentValue)
        {
            onThresholdReached.Invoke();
            thresholdReached = true;
        }
        else if (thresholdReached == true && threshold > currentValue)
        {
            onThresholdLost.Invoke();
            thresholdReached = false;
        }
    }
}
