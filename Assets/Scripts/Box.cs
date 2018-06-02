using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {
    private AudioSource thud;
    private Rigidbody2D rb2d;
    private Vector3 prevVelocity;
    
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        thud = GetComponent<AudioSource>();
        prevVelocity = rb2d.velocity;
	}
	
	void Update () {
        if (prevVelocity.y < -1f & Mathf.Abs(rb2d.velocity.y) < .0001f) thud.Play();
        prevVelocity = rb2d.velocity;
	}

    private void OnCollisionStay2D(Collision2D collision) {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player && player.lightOrShadow == PlayerController.PlayerType.Shadow) {
            if (Mathf.Abs(rb2d.velocity.x) > .01f && player.GetComponent<Rigidbody2D>().velocity.x*rb2d.velocity.x > 0)
                player.Push();
            else
                player.StopPushing();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player && player.lightOrShadow == PlayerController.PlayerType.Shadow)
            player.StopPushing();
    }
}
