using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
	
	public bool pressed = false;

	void OnTriggerEnter2D(Collider2D other) {
		pressed = true;
		// TODO 2018-01-20: some kind of animation ?
	}

	void OnTriggerExit2D(Collider2D other) {
		pressed = false;
	}
}
