using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchController : MonoBehaviour {


  public string nextScene;            // Takes in the name of the scene of next level
  public GameObject lightPlayer;
  public GameObject shadowPlayer;

  private Vector3 lightDoorPos;
  private Vector3 shadowDoorPos;

  // Use this for initialization
  void Start() {

    // Get the position of door objects
    GameObject lightDoor = GameObject.FindWithTag("LightDoor");
    GameObject shadowDoor = GameObject.FindWithTag("ShadowDoor");
    lightDoorPos = lightDoor.transform.position;
    shadowDoorPos = shadowDoor.transform.position;
  }
  
  void LateUpdate() {

    // Repeatedly check if player object's rectangle contains door position
    if (lightPlayer.GetComponent<Collider2D>().bounds.Contains(lightDoorPos)
      && shadowPlayer.GetComponent<Collider2D>().bounds.Contains(shadowDoorPos) ) {
      SceneManager.LoadScene(nextScene);
    }

  }
}
