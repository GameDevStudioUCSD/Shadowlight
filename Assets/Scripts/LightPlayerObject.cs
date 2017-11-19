using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlayerObject : PlayerObject {

    public GameObject castLight;
    public Material shadowMaterial;
    public GameObject background;
    public Shader shadowStencil;

    protected bool setShaders = false;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        keyUp = KeyCode.UpArrow;
        keyDown = KeyCode.DownArrow;
        keyLeft = KeyCode.LeftArrow;
        keyRight = KeyCode.RightArrow;
        Globals.lightPlayer = this.gameObject;
        Globals.castLight = castLight;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        if (!setShaders) {
            SetShaders();
            setShaders = true;
        }
    }

    protected virtual void SetShaders() {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        background.GetComponent<MeshRenderer>().material.shader = shadowStencil;
        foreach (GameObject gameObj in allObjects)
            if (gameObj.activeInHierarchy && gameObj.GetComponent<SpriteRenderer>()) {
                Material mat = gameObj.GetComponent<SpriteRenderer>().material;
                if (mat.name == shadowMaterial.name + " (Instance)")
                    mat.shader = shadowStencil;
            }
                
    }
}
