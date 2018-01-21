using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {

	//similar to pressure plate ?
	//but pressure plate needs continuous contact - has trigger exit function

	public bool pushed = false;

	//TODO 2018-01-20 connect this to circuit ? just a trigger for now
	    void OnTriggerEnter(Collider other) {
			//some animation 
		    pushed = true;
		}
		
}
