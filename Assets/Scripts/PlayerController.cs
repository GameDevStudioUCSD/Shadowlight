using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * Responsible for listening to inputs and moving the character.
 */
[RequireComponent(typeof(Animator), typeof(Rigidbody2D))]
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

    private Rigidbody2D rb2d = null;
    private Animator am = null;
    private SpriteRenderer sr = null;

    // The interactable object indicator
    private GameObject indicator = null;

    // The script from the interactable object
    private Interactable interactableScript = null;

    // For debugging purposes
    public bool invincible = false;

    private void Start()
    {
        Globals.gameOverScreen = GameObject.FindGameObjectWithTag("GameOverScreen");
        if (Physics2D.gravity.y >= -10) Physics2D.gravity = 3 * Physics2D.gravity;

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

        // Since we have the RequireComponent, this should never happen.
        Debug.Assert(rb2d != null, "PlayerController: Needs Rigidbody2D.", this);
        Debug.Assert(indicator != null, "PlayerController: Needs Indicator.", this);

        // Please change the values in the editor.
        Debug.Assert(inputHorizontal != "", "PlayerController: Horizontal input is empty.", this);
        Debug.Assert(inputJump != "", "PlayerController: Jump input is empty.", this);
        Debug.Assert(inputInteract != "", "PlayerController: Interact input is empty.", this);

        CameraZoom.instance.RegisterPlayer(transform);
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

        // Casts rays from the center of the object downward to check for ground
        Vector2 position = transform.TransformPoint(GetComponent<BoxCollider2D>().offset);
        Vector2 direction = Vector2.down;
        // The ray's length is slightly longer than half the size of the object
        float length = (GetComponent<BoxCollider2D>().bounds.size.y / 2) + 0.1f;
        // Offset the left and right rays by half of the character's size
        float offset = (GetComponent<BoxCollider2D>().bounds.size.x / 2) - 0.01f;

        Vector2 temp = new Vector2(offset, 0);

        // Cast three rays to check for ground collision
        RaycastHit2D[] hitCenter = Physics2D.RaycastAll(position, direction, length);
        RaycastHit2D[] hitRight = Physics2D.RaycastAll(position + temp, direction, length);
        RaycastHit2D[] hitLeft = Physics2D.RaycastAll(position - temp, direction, length);

        // Checks each Raycast to see if it collides with ground
        if (rayCastCheck(hitCenter) || rayCastCheck(hitRight) || rayCastCheck(hitLeft))
        {
            return true;
        }
        return false;
    }


    /** Checks if Raycast collides with an object that's not the self object*/
    private bool rayCastCheck(RaycastHit2D[] raycast)
    {
        foreach (RaycastHit2D ray in raycast)
        {
            if (ray.collider != null && ray.collider.gameObject != this.gameObject)
                return true;
        }
        return false;
    }


    /**
     * Called when the Shadow player touches the light, or any other time the player is killed.
     */
    public virtual void Die()
    {
        if (!invincible)
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
    }

    /**
     * Called from within the animator to turn off the sprite after the death animation.
     * Without this, the death animation will loop.
     */
    public void EndDeathAnimation()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    /**
     * Brings up the game over screen and calls Reload after 3 seconds.
     */
    public void GameOver()
    {
        Globals.gameOverScreen.GetComponent<SpriteRenderer>().enabled = true;
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
