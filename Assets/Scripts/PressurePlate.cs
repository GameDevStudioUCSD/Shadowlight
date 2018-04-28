using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PressurePlate : MonoBehaviour {

    public int objectsPressing = 0;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            objectsPressing++;
            platePressed.Invoke();

            renderer.sprite = pressedSprite;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            objectsPressing++;
            platePressed.Invoke();

            renderer.sprite = pressedSprite;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (objectsPressing > 0)
        {
            objectsPressing--;
            plateUnpressed.Invoke();

            renderer.sprite = unpressedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(objectsPressing > 0) {
            objectsPressing--;
            plateUnpressed.Invoke();

            renderer.sprite = unpressedSprite;
        }
    }
}
