using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(SpriteRenderer))]
public class Spring : MonoBehaviour {
    [SerializeField]
    private float force = 0;

    private Animator animator = null;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        // Should not be null because of the RequireComponent attribute.
        Assert.IsNotNull(animator, name + " requires an Animator component.");
	}

	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            animator.Play("Boing");
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(-rb.velocity.x, force);
        }
    }
}
