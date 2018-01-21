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

    if (Input.GetButton(inputJump)) {
      // TODO 2018-01-20: Jump!
      Debug.Log("Jump!", this);
    }

    rb2d.velocity = tmpVelocity;
  }
}
