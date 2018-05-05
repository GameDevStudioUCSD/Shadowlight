using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Responsible for listening to inputs and moving the character.
 */
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{

    // See Edit -> Project Settings -> Input for the input configuration.

    public enum PlayerType { Light, Shadow };

    public PlayerType lightOrShadow;

    // Controls horizontal movement.
    private string inputHorizontal = "";

    // Controls the jump action.
    private string inputJump = "";

    // Controlls the interact action
    private string inputInteract = "";

    // How fast should the character move horizontally.
    public float horizontalSpeed = 5.0f;

    // The strength of the character's jump
    public float jumpForce = 7.0f;

    // Amount of force needed to kill the player by crushing
    private float crushThreshold = 1000f;

    private Animator am = null;
    private BoxCollider2D bc2d = null;
    private Rigidbody2D rb2d = null;
    private SpriteRenderer sr = null;

    // The interactable object indicator
    private GameObject indicator = null;

    // The script from the interactable object
    private Interactable interactableScript = null;

    private ContactFilter2D jumpRaycastFilter;
    private RaycastHit2D[] jumpRaycastResult = null;

    private void Start()
    {
        Globals.gameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");

        if (lightOrShadow == PlayerType.Light)
        {
            inputHorizontal = "Light Horizontal";
            inputJump = "Light Jump";
            inputInteract = "Light Interact";
            Globals.lightPlayer = this.gameObject;
        }
        else if (lightOrShadow == PlayerType.Shadow)
        {
            inputHorizontal = "Shadow Horizontal";
            inputJump = "Shadow Jump";
            inputInteract = "Shadow Interact";
            Globals.shadowPlayer = this.gameObject;
        }

        rb2d = GetComponent<Rigidbody2D>();
        am = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        indicator = transform.Find("Indicator").gameObject;

        CameraZoom.instance.RegisterPlayer(transform);

        bc2d = GetComponent<BoxCollider2D>();
        // Do not filter anything except triggers.
        jumpRaycastFilter.NoFilter();
        jumpRaycastFilter.useTriggers = false;
        // We only need the existence of something.
        jumpRaycastResult = new RaycastHit2D[1];
    }

    private void Update()
    {
        if (!inputHorizontal.Equals(""))
        {

            Vector2 tmpVelocity = rb2d.velocity;

            // Horizontal movement.
            // TODO 2018-01-20: Maybe we can do gradual acceleration movement.
            tmpVelocity.x = Input.GetAxis(inputHorizontal) * horizontalSpeed;

            // Flip the sprite if the velocity changes
            if (tmpVelocity.x < 0)
            {
                sr.flipX = true;
            }
            else if (tmpVelocity.x > 0)
            {
                sr.flipX = false;
            }

            // Jump Movement
            // TODO 2018-01-29: Add friction to jump
            // Add a feeling of inertia
            if (Input.GetButtonDown(inputJump))
            {
                if (IsGrounded())
                {
                    tmpVelocity.y = jumpForce;
                    am.SetTrigger("jump");
                }
            }

            // Reset Key
            if (Input.GetKeyDown(KeyCode.R)) Reload();

            am.SetBool("moving", tmpVelocity.x != 0);
            am.SetFloat("yVelocity", tmpVelocity.y);

            rb2d.velocity = tmpVelocity;

            // Check if player can interact with object
            if(interactableScript != null && Input.GetButtonDown(inputInteract)) {
                interactableScript.interact.Invoke();
            }
        }
    }

    /** Checks if there is ground underneath the object */
    private bool IsGrounded()
    {
        // Calculate the ray.
        // It is basically a ray from left bottom to right bottom, with a little
        // bit of offset.
        Bounds bounds = bc2d.bounds;
        Vector2 origin = new Vector2(bounds.min.x, bounds.min.y - 0.05f);
        float distance = bounds.max.x - bounds.min.x;

        // We only want to know something is there.
        return 0 != Physics2D.Raycast(
            origin,
            Vector2.right,
            jumpRaycastFilter,
            jumpRaycastResult,
            distance
        );
    }

    /**
     * Called when the Shadow player touches the light, or any other time the player is killed.
     */
    public virtual void Die()
    {
        rb2d.velocity = Vector2.zero; //stop moving
        rb2d.gravityScale = 0; //stop falling
        gameObject.GetComponent<Collider2D>().enabled = false; //don't touch things anymore
        inputHorizontal = ""; //input stops working
        inputJump = ""; //can't jump anymore
        inputInteract = ""; //can't interact anymore
        am.SetTrigger("death"); //play death animation
        Invoke("GameOver", 1); //call GameOver() after one second
    }

    /**
     * Brings up the game over screen and calls Reload after 3 seconds.
     */
    public void GameOver()
    {
        Globals.gameOverScreen.GetComponent<SpriteRenderer>().enabled = true;
        Globals.gameOverScreen.GetComponentInChildren<Canvas>().enabled = true;
        Invoke("Reload", 3); //call Reload() after three seconds
    }

    /**
     *  Reload the scene instantly.
     */
    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player experiences a huge force from above, this kills them.
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
        if (rb && -rb.velocity.y * rb.mass >= crushThreshold) Die();

        Interactable interactable = collision.GetComponent<Interactable>();

        // Show interactable object indicator
        if (interactable != null)
        {
            indicator.SetActive(true);
            interactableScript = interactable;
            interactableScript.highlightTargets.Invoke();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        // Hide interactable object indicator
        if (collision.GetComponent<Interactable>() != null)
        {
            indicator.SetActive(false);
            interactableScript = null;
        }
    }
}
