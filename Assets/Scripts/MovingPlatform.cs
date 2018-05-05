using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {

    public List<Vector2> positions; //a list of 2D positions that the platform will move to
    private List<Vector3> positions3d; //a list of 3D positions generated from the 2D positions by Start() that is used internally
    private int positionIndex = 0; //the index of the position in the List that the platform is heading toward while moving, or the last it touched if stopped

    public float moveSpeed = 0.1f; //the distance the platform moves each frame
    public float loopWaitTime = 3f; //time the platform waits between loop directions
    private float loopTimer = 0f; //time remaining before the waiting platform starts moving again

    public enum State {Stopped, MovingForward, MovingBack, Waiting};
    public State movementState = State.Stopped; //how platform is currently moving
    public bool looping = false; //turn on to make the platform move back and forth
    private bool nextMoveIsBack = false;
    private Animator animator;

    void Start () {
        animator = gameObject.GetComponent<Animator>();

        positions3d = new List<Vector3>();
        positions3d.Add(transform.position); //starting position is part of the List
        //add each Vector2 position to the Vector3 List
        foreach (Vector2 position in positions) {
            positions3d.Add(position);
        }
    }
	
	void Update () {
        ChangeState();

        if (movementState == State.MovingForward) {
            //moves the platform towards the target position in the List
            transform.position = Vector3.MoveTowards(transform.position, positions3d[positionIndex], moveSpeed);
            //if the platform has reached the target position, move on to the next
            if (transform.position.Equals(positions3d[positionIndex])) positionIndex += 1;
        }
        else if (movementState == State.MovingBack) {
            //moves the platform towards the target position in the List
            transform.position = Vector3.MoveTowards(transform.position, positions3d[positionIndex], moveSpeed);
            //if the platform has reached the target position, move on to the next
            if (transform.position.Equals(positions3d[positionIndex])) positionIndex -= 1;
        }
    }

    /**
     * Helper method for Update(). Determines when the state neeeds to be changed,
     *   such as when the platform reaches its destination, or the wait timer runs out.
     *   Changes from the Stopped state are handled by the public methods below.
     */
    private void ChangeState() {
        if (movementState == State.Waiting) { //platform is looping and waiting to start moving again
            loopTimer -= Time.deltaTime; //decrease countdown to when it starts moving again
            if (loopTimer <= 0) { //if done waiting
                if (nextMoveIsBack) MoveBack();
                else MoveForward();
            }
        }
        else if (movementState == State.MovingForward) {
            if (positionIndex > positions3d.Count-1) { //reached the last position in the List
                positionIndex = positions3d.Count-1;
                nextMoveIsBack = true; //move back next time
                if (looping) {
                    loopTimer = loopWaitTime; //reset wait timer
                    movementState = State.Waiting; //wait before moving back
                    animator.SetBool("Moving", false);
                }
                else {
                    movementState = State.Stopped; //stop moving
                    animator.SetBool("Moving", false);
                }
            }
        }
        else if (movementState == State.MovingBack) {
            if (positionIndex < 0) { //reached the first position in the List
                positionIndex = 0;
                nextMoveIsBack = false; //move forward next time
                if (looping) {
                    loopTimer = loopWaitTime; //reset wait timer
                    movementState = State.Waiting; //wait before moving back
                    animator.SetBool("Moving", false);
                }
                else {
                    movementState = State.Stopped; //stop moving
                    animator.SetBool("Moving", false);
                }
            }
        }
    }

    /**
     * Call this method to set the platform to move back and forth, and if it was not already moving,
     *   it will start moving.
     */
    public void MoveWithLoop() {
        looping = true;
        if (movementState == State.Stopped) { //platform was not already moving, so pick direction to move
            if (nextMoveIsBack) MoveBack(); //move back
            else MoveForward(); //move forward
        }
    }

    /**
     * Call this method to stop the platform, regardless of which direction it was going and whether
     *   or not it was looping.
     */
    public void Stop() {
        looping = false;
        //prepares the index for when it starts moving again
        if (movementState == State.MovingForward) positionIndex -= 1;
        else if (movementState == State.MovingBack) positionIndex += 1;
        movementState = State.Stopped;
        animator.SetBool("Moving", false);
    }

    /**
     * Call this method to start the platform moving between the positions in the List in a forward direction,
     *   without looping.
     */
    public void MoveForward() {
        positionIndex += 1; //sets the index of the destination position in the List positions3d
        movementState = State.MovingForward;
        animator.SetBool("Moving", true);
    }

    /**
     * Call this method to start the platform moving between the positions in the List in a backward direction,
     *   without looping.
     */
    public void MoveBack() {
        positionIndex -= 1; //sets the index of the destination position in the List positions3d
        movementState = State.MovingBack;
        animator.SetBool("Moving", true);
    }
}
