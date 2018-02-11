using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour {
    public bool isLit;
    public GameObject castLight;

    protected CastLight lightScript;
    protected Animator animator;
    
	void Start() {
        lightScript = gameObject.GetComponent<CastLight>();
        animator = gameObject.GetComponent<Animator>();
        if (!isLit) lightScript.enabled = false; //turns off light if candle starts unlit
    }

    /**
     *  Turns on this candle's light scripting.
     */
    public void Light() {
        castLight.SetActive(true); //turn on candle's light
        lightScript.enabled = true; //start calculating mesh shape
        animator.SetTrigger("Light");
    }

    /**
     *  Turns off this candle's light scripting.
     */
    public void Extinguish() {
        castLight.SetActive(false); //turn off light
        lightScript.enabled = false; //stop calculating mesh shape (for efficiency)
        animator.SetTrigger("Extinguish");
    }
}
