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

    // Controls the interact action
    private string inputInteract = "";

    // Controls the climbing up action
    private string inputClimbUp = "";

    // Controls the climbing down action
    private string inputClimbDown = "";

    // How fast should the character move horizontally.
    public float horizontalSpeed = 5.0f;

    // The strength of the character's jump
    public float jumpForce = 7.0f;

    // Amount of force needed to kill the player by crushing
    private float crushThreshold = 1000f;

    private Vector2 tmpVelocity;

    private Animator am = null;
    private BoxCollider2D bc2d = null;
    private Rigidbody2D rb2d = null;
    private SpriteRenderer sr = null;
    private bool hasDied = false;

    // The interactable object indicator
    private GameObject indicator = null;

    // The script from the interactable object
    private Interactable interactableScript = null;

    private GrowingPlant plantScript = null;
    private bool canClimb = false;
    private bool isClimbing = false;

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
            inputClimbUp = "Light Climb Up";
            inputClimbDown = "Light Climb Down";
            Globals.lightPlayer = this.gameObject;
        }
        else if (lightOrShadow == PlayerType.Shadow)
        {
            inputHorizontal = "Shadow Horizontal";
            inputJump = "Shadow Jump";
            inputInteract = "Shadow Interact";
            inputClimbUp = "Shadow Climb Up";
            inputClimbDown = "Shadow Climb Down";
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
        // Quickly change scenes for debugging purposes
        if (Input.GetKeyDown(KeyCode.N))
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
        }

        if (!inputHorizontal.Equals(""))
        {

            tmpVelocity = rb2d.velocity;

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
                if (canClimb)   // Start Climbing
                {
                    tmpVelocity.y = 0;
                    isClimbing = true;
                }
                else if (IsGrounded() && !isClimbing)
                {
                    tmpVelocity.y = jumpForce;
                    am.SetTrigger("jump");
                }
            }

            // Climb Movement
            if (isClimbing) {
              Climb(transform.position.y);
            }

            am.SetFloat("yVelocity", tmpVelocity.y);
            am.SetBool("moving", tmpVelocity.x != 0);
            rb2d.velocity = tmpVelocity;

            // Reset Key
            if (Input.GetKeyDown(KeyCode.R)) Reload();

            // Check if player can interact with object
            if (interactableScript != null && Input.GetButtonDown(inputInteract)) {
                interactableScript.interact.Invoke();
            }
        }
    }

    /** Climb a plant */
    private void Climb(float startY)
    {
        am.SetBool("climbing", true);
        rb2d.gravityScale = 0;
        // Move up the plant
        if (canClimb)
        {
            if (Input.GetButton(inputClimbUp))
            {
                am.SetBool("climbing-motion", true);
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.04f, transform.position.z);
            }
            else if (Input.GetButton(inputClimbDown))
            {
                // Move down the plant
                if (transform.position.y >= startY)
                {
                    am.SetBool("climbing-motion", true);
                    transform.position = new Vector3(transform.position.x, transform.position.y - 0.03f, transform.position.z);
                }
            }
            // Pausing while climbing the plant
            else
            {
                am.SetBool("climbing-motion", false);
            }
        }
        else {
            am.SetBool("climbing-motion", false);
            if (Input.GetButton(inputHorizontal))
            {
                // Jump and get off plant
                isClimbing = false;

                rb2d.gravityScale = 1.0f;
                am.SetBool("climbing", isClimbing);
                am.SetBool("climbing-motion", false);
                tmpVelocity.y = 10;
                am.SetTrigger("jump");
                am.SetFloat("yVelocity", tmpVelocity.y);
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
        if (!hasDied) {
            rb2d.velocity = Vector2.zero; //stop moving
            rb2d.gravityScale = 0; //stop falling
            gameObject.GetComponent<Collider2D>().enabled = false; //don't touch things anymore
            inputHorizontal = ""; //input stops working
            inputJump = ""; //can't jump anymore
            inputInteract = ""; //can't interact anymore
            am.SetTrigger("death"); //play death animation
            GetComponent<AudioSource>().Play();
            hasDied = true;
            Invoke("GameOver", 1); //call GameOver() after one second
        }
    }

    /**
     * Brings up the game over screen and calls Reload after 3 seconds.
     */
    public void GameOver()
    {
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
        }

        plantScript = collision.GetComponent<GrowingPlant>();
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        // To check if the plant is fully grown before player can climb on it
        if (plantScript != null && plantScript.finishedGrowing)
        {
            canClimb = true;
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

        // Exit climbing
        if (collision.GetComponent<GrowingPlant>() != null)
        {
            canClimb = false;
        }
    }
}
