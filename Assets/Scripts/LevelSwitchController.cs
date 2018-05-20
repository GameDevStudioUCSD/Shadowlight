using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchController : MonoBehaviour {


  public string nextScene; // The name of the next level scene
  public GameObject lightPlayer;
  public GameObject shadowPlayer;

  private Bounds lightDoorBounds;
  private Bounds shadowDoorBounds;
    private GameObject lightDoor;
    private GameObject shadowDoor;
    private bool hasLoaded = false;

  void Start() {

    // Get the position of door objects
    lightDoor = GameObject.FindWithTag("LightDoor");
    shadowDoor = GameObject.FindWithTag("ShadowDoor");
    lightDoorBounds = lightDoor.GetComponent<Collider2D>().bounds;
    shadowDoorBounds = shadowDoor.GetComponent<Collider2D>().bounds;
  }

  void LateUpdate() {
    // This is in late update because the bound checking is not as important as
    // updating game physics and animation.

    // TODO 2018-01-13: Right now, this is checking almost every frame. It might
    // be better to split the actual checking of the character inside door and
    // the switching level into separate component. That way, the component can
    // check the character bound on OnCollider*2D functions.

    Bounds lightPlayerBounds = lightPlayer.GetComponent<Collider2D>().bounds;
    Bounds shadowPlayerBounds = shadowPlayer.GetComponent<Collider2D>().bounds;

    // This checks to make sure that the character is completely inside the
    // door's collider.
    if (!hasLoaded && lightDoorBounds.Contains(lightPlayerBounds.min)
      && lightDoorBounds.Contains(lightPlayerBounds.max)
      && shadowDoorBounds.Contains(shadowPlayerBounds.min)
      && shadowDoorBounds.Contains(shadowPlayerBounds.max)) {
            lightDoor.GetComponent<Animator>().SetTrigger("Open");
            shadowDoor.GetComponent<Animator>().SetTrigger("Open");
            lightDoor.GetComponent<AudioSource>().Play();
            hasLoaded = true;
            //Globals.lightPlayer.GetComponent<PlayerController>().enabled = false;
            //Globals.shadowPlayer.GetComponent<PlayerController>().enabled = false;
            StartCoroutine("LoadScene", 0.5f);
    }
  }

    IEnumerator LoadScene(float delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextScene);
    }
}
