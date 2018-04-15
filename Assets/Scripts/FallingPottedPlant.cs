using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(DistanceJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class FallingPottedPlant : MonoBehaviour {

	private Animator anim = null;
	private DistanceJoint2D joint = null;
	private Rigidbody2D rb2d = null;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		joint = GetComponent<DistanceJoint2D> ();
		rb2d = GetComponent<Rigidbody2D> ();

		GameObject line = transform.GetChild (0).gameObject;
		Vector3 lineScale = line.transform.localScale;
		lineScale.y = (joint.anchor - joint.connectedAnchor).magnitude;
		line.transform.localScale = lineScale;
		line.transform.position += new Vector3(0.0f, lineScale.y / 2.0f * 0.04f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("yVelocity", rb2d.velocity.y);
	}

	public void Fall() {
		joint.enabled = false;
	}
}
