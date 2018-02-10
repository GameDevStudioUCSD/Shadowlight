using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShadowPlayerObject : PlayerObject {

	// Use this for initialization
	protected override void Start () {
        base.Start();

        //assign Shadow's controls to the WASD keys
        keyUp = KeyCode.W;
        keyDown = KeyCode.S;
        keyLeft = KeyCode.A;
        keyRight = KeyCode.D;

        //assign Global variables
        Globals.shadowPlayer = this.gameObject;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    /**
     * Called when the Shadow player touches the light.
     * Should cause a game over, but is currently TODO.
     */
    public virtual void Die() {
        //TODO
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
