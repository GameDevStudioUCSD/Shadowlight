using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxTopTrigger : MonoBehaviour {
    /**
     * This class goes on a trigger on top of objects that should move other objects on top of them.
     * If the object has a Dynamic Rigidbody, its children that also have Rigidbodies will not move with it.
     * Code here makes objects a child of this one if it has a Kinematic Rigidbody, and moves them
     *   manually if the object has a Dynamic Rigidbody.
     */

    public List<Transform> movedObjects; //list of objects in the trigger
    private Vector3 lastPosition; //position of the object last frame
    private bool isDynamic = false; //whether or not the object's Rigidbody is Dynamic
    
	void Start () {
        lastPosition = transform.parent.position; //initialize position
        //Check if Rigidbody is Dynamic
        RigidbodyType2D rbtype = GetComponentInParent<Rigidbody2D>().bodyType;
        if (rbtype == RigidbodyType2D.Dynamic) isDynamic = true;
    }
	
    /**
     * Moves objects in the trigger with the object the trigger is on top of.
     */
	void LateUpdate () {
        //If Rigidbody is Dynamic, objects on top of this object are moved the same distance as it
		if (isDynamic && movedObjects.Count != 0) {
            Vector3 diff = transform.parent.position - lastPosition;
            foreach (Transform movedObject in movedObjects) {
                PlayerController player = movedObject.GetComponent<PlayerController>();
                if (movedObject.GetComponent<PlayerController>()) movedObject.position += diff;
            }
        }
        lastPosition = transform.parent.position; //update position
	}

    /**
     * When an object enters the trigger, starts moving them with the object.
     */
    public void OnTriggerEnter2D(Collider2D other) {
        Transform obj = other.transform;
        if (obj && !other.isTrigger && !movedObjects.Contains(obj)) {
            movedObjects.Add(obj); //add object to list of objects to move
            if (!isDynamic && !obj.parent) obj.transform.SetParent(transform.parent); //if Rigidbody is Kinematic, make the object a child
        }
    }

    /**
     * If an object exiting the trigger is a player, stops moving them with the object.
     */
    public void OnTriggerExit2D(Collider2D other) {
        if (movedObjects.Count != 0) { //no point if there are no objects on top of the object already
            Transform obj = other.transform;
            if (obj && movedObjects.Contains(obj)) {
                if (!isDynamic) obj.transform.SetParent(null); //if Rigidbody is Kinematic, remove the object as a child
                movedObjects.Remove(obj); //remove object from list of objects to move
            }
        }
    }
}
