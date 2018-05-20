using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PressurePlate : MonoBehaviour {

    private int numObjects = 0; // Number of objects on the plate
    public UnityEvent platePressed = null;
    public UnityEvent plateUnpressed = null;

    public Sprite unpressedSprite = null;
    public Sprite pressedSprite = null;

    private new SpriteRenderer renderer = null;
    private AudioSource click;

    private void Start() {
        renderer = GetComponent<SpriteRenderer> ();
        click = GetComponent<AudioSource>();
        Assert.IsNotNull(renderer, name + " should have SpriteRenderer");
        renderer.sprite = unpressedSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            numObjects++;
            platePressed.Invoke();
            click.Play();
            renderer.sprite = pressedSprite;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (numObjects > 0)
        {
            numObjects--;
            if (numObjects == 0)
            {
                plateUnpressed.Invoke();
                renderer.sprite = unpressedSprite;
            }
        }
    }
}
