using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 *  Generic class for controlling a player character. Extended to create the
 *  unique Light and Shadow characters.
 */
public class PlayerObject : MonoBehaviour {
    protected KeyCode keyUp;
    protected KeyCode keyDown; //currently unused
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
        Move();
        if (CheckJump()) Jump();
    }

    /**
     * Controls the players' movement
     */
    protected void Move() {
        //Move based on keyboard input
        if (Input.GetKey(keyRight) && !Input.GetKey(keyLeft)) rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        else if (Input.GetKey(keyLeft) && !Input.GetKey(keyRight)) rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);

        //abrupt stops when the keys are lifted
        else rb.velocity = new Vector2(0, rb.velocity.y);
    }

    /**
     * Checks if the player is touching the ground before allowing them to jump
     */
    protected bool CheckJump() {
        if (Input.GetKeyDown(keyUp)) {
            RaycastHit2D[] raycast = Physics2D.RaycastAll(transform.position, Vector2.down, 0.55f);
            if (raycast.Length > 0) {
                foreach (RaycastHit2D ray in raycast) {
                    if (ray.collider.gameObject != this.gameObject) return true;
                }
            }
        }
        return false;
    }

    /**
     * Jumps by applying an upward force
     */
    protected void Jump() {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce);
    }
}
