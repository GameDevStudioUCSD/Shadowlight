using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowPlayerObject : PlayerObject {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        keyUp = KeyCode.W;
        keyDown = KeyCode.S;
        keyLeft = KeyCode.A;
        keyRight = KeyCode.D;
    }
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}
}
