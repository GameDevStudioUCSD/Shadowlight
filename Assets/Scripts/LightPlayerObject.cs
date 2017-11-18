using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayerObject : PlayerObject {

	// Use this for initialization
	protected override void Start () {
        base.Start();
        keyUp = KeyCode.UpArrow;
        keyDown = KeyCode.DownArrow;
        keyLeft = KeyCode.LeftArrow;
        keyRight = KeyCode.RightArrow;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
    }
}
