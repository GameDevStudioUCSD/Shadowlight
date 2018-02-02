using System.Collections;
using UnityEngine;

/**
 * Responsible for listening to inputs and moving the character.
 */
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

  // See Edit -> Project Settings -> Input for the input configuration.

  // Controls horizontal movement.
  public string inputHorizontal = "";

  // Controls the jump action.
  public string inputJump = "";

  // How fast should the character move horizontally.
  public float horizontalSpeed = 1.0f;

  // The strength of the character's jump
  public float jumpForce = 5f;

  // How rapidly the character should fall
  public float fallMulti = 2f;

  // How varied the character's jumps could be
  public float jumpMulti = 2f;

  // The layers that make up the ground (currently set to Default)
  public LayerMask groundLayer;

  private Rigidbody2D rb2d = null;

  private void Start() {

    rb2d = GetComponent<Rigidbody2D>();
    // Since we have the RequireComponent, this should never happen.
    Debug.Assert(rb2d != null, "PlayerController: Needs Rigidbody2D.", this);

    // Please change the values in the editor.
    Debug.Assert(inputHorizontal != "", "PlayerController: Horizontal input is empty.", this);
    Debug.Assert(inputJump != "", "PlayerController: Jump input is empty.", this);

  }

  private void Update() {

    Vector2 tmpVelocity = rb2d.velocity;

    // Horizontal movement.
    // TODO 2018-01-20: Maybe we can do gradual acceleration movement.
    tmpVelocity.x = Input.GetAxis(inputHorizontal) * horizontalSpeed;

    // Jump Movement
    // TODO 2018-01-29: Add friction to jump
    // Once player jumps forward, they should not be allowed to move backwards freely in air
    if (Input.GetButtonDown(inputJump)) {
      tmpVelocity.y = Jump();
    }

    // Character falls faster than it jump
    if (tmpVelocity.y < 0) {
      tmpVelocity.y += Physics2D.gravity.y * (fallMulti) * Time.deltaTime;
    }
    // Character's jump is based on how long the jump button is held down for
    else if (tmpVelocity.y > 0 && !Input.GetButton(inputJump)) {
      tmpVelocity.y += Physics2D.gravity.y * (jumpMulti) * Time.deltaTime;
    }

    rb2d.velocity = tmpVelocity;
  }

  /** Checks if there is ground underneath the object*/
  bool IsGrounded() {

    // Casts a ray from the center of the object downward to check for ground
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;
    // The ray's length is slightly longer than half the size of the object
    float distance = (GetComponent<BoxCollider2D>().bounds.size.y / 2) + 0.02f;

    // TODO 2018-02-01: Make Raycast work with sloped surfaces
    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
    Debug.DrawRay(position, direction, Color.blue);
    if (hit.collider != null) {
      return true;
    }
    return false;
  }

  /** Handles the character jumping (Checks if grounded)*/
  float Jump() {

    if (!IsGrounded()) {
      return 0;
    }
    return jumpForce;
    }
  }
}
