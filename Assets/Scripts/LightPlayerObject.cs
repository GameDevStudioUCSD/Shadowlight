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

        //assign Light's controls to the Arrow keys
        keyUp = KeyCode.UpArrow;
        keyDown = KeyCode.DownArrow;
        keyLeft = KeyCode.LeftArrow;
        keyRight = KeyCode.RightArrow;

        //assign Global variables
        Globals.lightPlayer = this.gameObject;
        Globals.castLight = castLight;
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
        
        //turns everything outside of the light black
        if (!setShaders) {
            SetShaders();
            setShaders = true;
        }
    }

    /**
     * Modifies every object with the shadow material to be black outside of the light.
     */
    protected virtual void SetShaders() {
        GameObject[] allObjects = FindObjectsOfType<GameObject>(); //gets all objects in the scene
        background.GetComponent<MeshRenderer>().material.shader = shadowStencil; //makes the background only visible in light
        foreach (GameObject gameObj in allObjects)
            if (gameObj.activeInHierarchy && gameObj.GetComponent<SpriteRenderer>()) {
                Material mat = gameObj.GetComponent<SpriteRenderer>().material;
                if (mat.name == shadowMaterial.name + " (Instance)") //detects instances of the shadow material
                    mat.shader = shadowStencil; //changes the materia's shader so it is only visible in light
            }
                
    }
}
