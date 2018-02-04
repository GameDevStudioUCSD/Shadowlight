using System.Collections;
using UnityEngine;

/**
 * Responsible for listening to inputs and moving the character.
 */
[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour {

  // See Edit -> Project Settings -> Input for the input configuration.

  // Controls horizontal movement.
  public string inputHorizontal = "";

  // Controls the jump action.
  public string inputJump = "";

  // How fast should the character move horizontally.
  public float horizontalSpeed = 5.0f;

  // The strength of the character's jump
  public float jumpForce = 5.0f;

  // How rapidly the character should fall
  public float fallMulti = 1.1f;

  // How varied the character's jumps could be
  public float jumpMulti = 2.0f;

  // The layers that make up the ground (currently set to Default)
  public LayerMask groundLayer;

  private Rigidbody2D rb2d = null;
  private Animator am = null;
  private SpriteRenderer sr = null;

  private void Start() {

    rb2d = GetComponent<Rigidbody2D>();
    am = GetComponent<Animator>();
    sr = GetComponent<SpriteRenderer>();
    // Since we have the RequireComponent, this should never happen.
    Debug.Assert(rb2d != null, "PlayerController: Needs Rigidbody2D.", this);

    // Please change the values in the editor.
    Debug.Assert(inputHorizontal != "", "PlayerController: Horizontal input is empty.", this);
    Debug.Assert(inputJump != "", "PlayerController: Jump input is empty.", this);

    //Set groundLayer to default
    groundLayer = 1 << LayerMask.NameToLayer("Default");

  }

  private void Update() {

    Vector2 tmpVelocity = rb2d.velocity;

    // Horizontal movement.
    // TODO 2018-01-20: Maybe we can do gradual acceleration movement.
    tmpVelocity.x = Input.GetAxis(inputHorizontal) * horizontalSpeed;

    // Flip the sprite if the velocity changes
    if (tmpVelocity.x < 0) {
      sr.flipX = true;
    }
    else if (tmpVelocity.x > 0) {
      sr.flipX = false;
    }

    // Jump Movement
    // TODO 2018-01-29: Add friction to jump
    // Add a feeling of inertia
    if (Input.GetButtonDown(inputJump)) {
      if (IsGrounded()) {
        tmpVelocity.y = jumpForce;
        am.SetTrigger("jump");
      }
    }

    if (tmpVelocity.y < 0) {
      tmpVelocity.y += Physics2D.gravity.y * (fallMulti) * Time.deltaTime;
    }
    // Character's jump is based on how long the jump button is held down for
    else if (tmpVelocity.y > 0 && !Input.GetButton(inputJump)) {
      tmpVelocity.y += Physics2D.gravity.y * (jumpMulti) * Time.deltaTime;
    }

    am.SetBool("moving", tmpVelocity.x != 0);
    am.SetFloat("yVelocity", tmpVelocity.y);

    rb2d.velocity = tmpVelocity;
  }

  /** Checks if there is ground underneath the object*/
  private bool IsGrounded() {

    // Casts a ray from the center of the object downward to check for ground
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;
    // The ray's length is slightly longer than half the size of the object
    float distance = (GetComponent<BoxCollider2D>().bounds.size.y / 2) + 0.02f;

    // TODO 2018-02-01: Make Raycast work with sloped surfaces
    RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, groundLayer);
    if (hit.collider != null) {
      return true;
    }
    return false;
  }
}
