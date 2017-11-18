using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Generic class for controlling a player character. Extended to create the
 *  unique Light and Shadow characters.
 */
public class PlayerObject : MonoBehaviour {
    protected KeyCode keyUp;
    protected KeyCode keyDown;
    protected KeyCode keyLeft;
    protected KeyCode keyRight;
    protected Rigidbody2D rb;
    
    public float moveSpeed = 10f;
    public float jumpForce = 1000f;

	// Use this for initialization
	protected virtual void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	protected virtual void Update () {
    }

    protected virtual void FixedUpdate() {
        Move();
        if (CheckJump()) Jump();
    }

    protected void Move() {
        //Move based on keyboard input
        if (Input.GetKey(keyRight) && !Input.GetKey(keyLeft)) rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        else if (Input.GetKey(keyLeft) && !Input.GetKey(keyRight)) rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        else rb.velocity = new Vector2(0, rb.velocity.y);
    }

    protected bool CheckJump() {
        if (Input.GetKeyDown(keyUp)) {
            if (rb.velocity.y == 0)
                return true;
        }
        return false;
    }

    protected void Jump() {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
    }
}
