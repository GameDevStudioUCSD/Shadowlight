using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class Button : MonoBehaviour {

	public bool pushed = false;
	public UnityEvent buttonPressed = null;

	public Sprite unpushedSprite = null;
	public Sprite pushedSprite = null;

	private new SpriteRenderer renderer = null;

	private void Start() {
		renderer = GetComponent<SpriteRenderer> ();
		Assert.IsNotNull(renderer, name + " should have SpriteRenderer");
	    renderer.sprite = unpushedSprite;

    }
	
    private void OnTriggerEnter2D(Collider2D other) {
        if (!pushed) {
            pushed = true;
            buttonPressed.Invoke();
            GetComponent<AudioSource>().Play();

            renderer.sprite = pushedSprite;
        }
	}
}
