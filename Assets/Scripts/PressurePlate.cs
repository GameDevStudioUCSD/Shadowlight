using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PressurePlate : MonoBehaviour {

    float offset = 0.13f;

    public bool pressed = false;
    public UnityEvent platePressed = null;
    public UnityEvent plateUnpressed = null;

    public Sprite unpressedSprite = null;
    public Sprite pressedSprite = null;

    private new SpriteRenderer renderer = null;

    private void Start() {
        renderer = GetComponent<SpriteRenderer> ();
        Assert.IsNotNull(renderer, name + " should have SpriteRenderer");
        renderer.sprite = unpressedSprite;
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if (!pressed)
        {
            pressed = true;
            platePressed.Invoke();

            renderer.sprite = pressedSprite;
            transform.position = new Vector2(transform.position.x, transform.position.y - offset);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(pressed) {
            pressed = false;
            plateUnpressed.Invoke();

            renderer.sprite = unpressedSprite;
            transform.position = new Vector2(transform.position.x, transform.position.y + offset);
        }
    }
}
