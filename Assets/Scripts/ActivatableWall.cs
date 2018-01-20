using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 */
public class ActivatableWall : MonoBehaviour {

	// TODO 2018-01-20: Debate publicness of this variable.
	public bool hidden = false;

	/**
	 * Toggles the wall's state. If the wall is hidden, it shows itself. Likewise, if the wall is shown, it hides.
	 * 
	 * TODO 2018-01-20: Actually toggle the wall.
	 */
	public void Toggle() {
		hidden = !hidden;
	}
}
