using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Class for the prism object, which switches the current positions of Light
 * and Shadow when it is interacted by with either character.
 */
public class Prism : MonoBehaviour {
    public void SwitchPositions() {
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
}
