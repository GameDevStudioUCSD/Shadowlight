using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWall : MonoBehaviour {
    public Vector3 moveOffset;
    public float moveSpeed = 1;
    public bool reverse = false;

    private Vector3 originalPosition;
    private Vector3 movedPosition;
    private bool moved = false;
    private bool moving = false;

    
	void Start () {
        //records the unmoved position of the wall
        originalPosition = transform.position;
        movedPosition = originalPosition + moveOffset;

        if (reverse)
        {
            movedPosition = originalPosition - moveOffset;
        }
	}
	
	void Update () {
		if (moving) {
            //first movement
            if (!moved) {
                //still moving
                if (!transform.position.Equals(movedPosition)) {
                    transform.position = Vector3.MoveTowards(transform.position, movedPosition, moveSpeed);
                }
                //done moving
                else {
                    moving = false;
                    moved = true;
                }
            }
            //return to original position
            else {
                //still moving
                if (!transform.position.Equals(originalPosition)) {
                    transform.position = Vector3.MoveTowards(transform.position, originalPosition, moveSpeed);
                }
                //done moving
                else {
                    moving = false;
                    moved = false;
                }
            }
        }
	}

    /**
     *  Called to start the wall moving to its new position
     */
    public void Move() {
        moved = false; //in case method is called before moving is finished
        moving = true;
    }

    /**
     * Called to start the wall moving back to its original position
     */
     public void Return() {
        moved = true; //in case method is called before moving is finished
        moving = true;
    }
}
