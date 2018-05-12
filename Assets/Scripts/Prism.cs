using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

/**
 * Class for the prism object, which switches the current positions of Light
 * and Shadow when it is interacted by with either character.
 */
[RequireComponent(typeof(SpriteRenderer))]
public class Prism : MonoBehaviour {
    private Animator animator = null;
    private SpriteRenderer flashSprite = null;

    void Start() {
        animator = GetComponent<Animator>();
        // Should not be null because of the RequireComponent attribute.
        Assert.IsNotNull(animator, name + " requires an Animator component.");

        GameObject flash = flash = transform.Find("Flash").gameObject;
        flashSprite = flash.GetComponent<SpriteRenderer>();
    }

    public void SwitchPositions() {
        animator.Play("Active");
        Invoke("MovePlayers", 1.4f);

    }

    void MovePlayers() {
        Flash();

        // Find Light and Shadow in the scene
        Transform lightPlayer = GameObject.Find("LightPlayer").transform;
        Transform shadowPlayer = GameObject.Find("ShadowPlayer").transform;

        // Hide the interactability indicator
        lightPlayer.Find("Indicator").gameObject.SetActive(false);
        shadowPlayer.Find("Indicator").gameObject.SetActive(false);

        // Switch Light and Shadow's positions
        Vector3 lightPlayerPosition = lightPlayer.position;
        lightPlayer.position = shadowPlayer.position;
        shadowPlayer.position = lightPlayerPosition;
    }

    void Flash() {
        
    }
}
