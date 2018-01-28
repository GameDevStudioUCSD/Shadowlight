using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {


	public bool pushed = false;

	public Sprite unpushedSprite = null;
	public Sprite pushedSprite = null;

	private SpriteRenderer renderer;

	private void Start() {
		renderer = GetComponent<SpriteRenderer> ();
	    renderer.sprite = unpushedSprite;

	    }
		
	    void OnTriggerEnter2D(Collider2D other) {
			 
		    pushed = true;
		    renderer.sprite = pushedSprite;
		}
		
}
