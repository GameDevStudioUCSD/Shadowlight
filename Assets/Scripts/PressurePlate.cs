using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour {
	public bool pressurePlateOn;
	// Use this for initialization
	void Start () {
		
	}

	void OnTriggerEnter(Collider other) {
		pressurePlateOn = true;
		//some kind of animation ?
	}

	void OnTriggerExit(Collider other) {
		pressurePlateOn = false;
	}
	// Update is called once per frame
	void Update () {
		
	}
}
