using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DistanceJoint2D))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class FallingPottedPlant : MonoBehaviour {

	private Animator anim = null;
	private DistanceJoint2D joint = null;
	private LineRenderer lineRenderer = null;
	private Rigidbody2D rb2d = null;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		joint = GetComponent<DistanceJoint2D> ();
		lineRenderer = GetComponent<LineRenderer> ();
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("yVelocity", rb2d.velocity.y);
		lineRenderer.SetPosition (0, transform.position + (Vector3)joint.anchor);
		lineRenderer.SetPosition (1, joint.connectedAnchor);
	}

	public void Fall() {
		joint.enabled = false;
		lineRenderer.enabled = false;
	}
}
