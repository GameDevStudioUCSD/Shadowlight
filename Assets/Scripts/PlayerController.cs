using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
  public float jumpForce = 7.0f;

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

    //Set groundLayer to all layers except for Ignore Raycast
    groundLayer = ~(1 << LayerMask.NameToLayer("Ignore Raycast"));

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

    am.SetBool("moving", tmpVelocity.x != 0);
    am.SetFloat("yVelocity", tmpVelocity.y);

    rb2d.velocity = tmpVelocity;
  }

  /** Checks if there is ground underneath the object*/
  private bool IsGrounded() {

    // Casts rays from the center of the object downward to check for ground
    Vector2 position = transform.position;
    Vector2 direction = Vector2.down;
    // The ray's length is slightly longer than half the size of the object
    float length = (GetComponent<BoxCollider2D>().bounds.size.y / 2) + 0.02f;
    // Offset the left and right rays by half of the character's size
    float offset = (GetComponent<BoxCollider2D>().bounds.size.x / 2) - 0.01f;

    Vector2 temp = new Vector2(offset, 0);

    // Cast three rays to check for ground collision
    RaycastHit2D hitCenter = Physics2D.Raycast(position, direction, length, groundLayer);
    RaycastHit2D hitRight = Physics2D.Raycast(position + temp, direction, length, groundLayer);
    RaycastHit2D hitLeft = Physics2D.Raycast(position - temp, direction, length, groundLayer);
    if (hitCenter.collider != null || hitLeft.collider != null || hitRight.collider != null) {
      return true;
    }
    return false;
  }


    /**
     * Called when the Shadow player touches the light.
     * Should cause a game over, but is currently TODO.
     */
    public virtual void Die() {
        if (inputHorizontal == "Shadow Horizontal") {
            //TODO
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
