using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
	
	public bool pressed = false;

	void OnTriggerEnter(Collider other) {
		pressed = true;
		// TODO 2018-01-20: some kind of animation ?
	}

	void OnTriggerExit(Collider other) {
		pressed = false;
	}
}
