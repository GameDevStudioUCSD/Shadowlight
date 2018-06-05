using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EntryDoor : MonoBehaviour {
    private Animator anim = null;
    SpriteRenderer sprite = null;

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        anim = GetComponent<Animator>();
	}

    private void Update()
    {
        Invoke("PlayAnimation", 0.5f);
    }

    private void Disappear() {
        sprite.enabled = false;
    }

    private void PlayAnimation() {
        anim.Play("Close");
        Invoke("Disappear", 0.5f);
    }
}
