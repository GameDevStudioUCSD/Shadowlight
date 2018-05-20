using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class for a plant that grows when in sunlight
 */
public class GrowingPlant : MonoBehaviour {
    Animator anim;
    public bool inRange;

	void Start () {
        anim = GetComponent<Animator>();
    }
	
	void Update () {
		
	}

    /**
     * Grows the plant when light is touching it
     */
    public void Grow()
    {
        if (anim.gameObject.activeSelf) {
            anim.SetTrigger("LightTouching");
        }
    }

    /**
     * Shrinks the plant back to smallest size
     * TODO 2018-10-03: Detect when to shrink
     */
    public void Shrink()
    {
        anim.ResetTrigger("LightTouching");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inRange = true;
    }
}
