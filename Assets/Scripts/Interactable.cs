using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * Script to attach to all interactable objects. Able to assign and invoke
 * the interact event.
 */
public class Interactable : MonoBehaviour {
    public UnityEvent interact = null;
    public UnityEvent highlightTargets = null;
}