using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSwitchController : MonoBehaviour {


  public string nextScene;            // Takes in the name of the scene of next level
  public GameObject lightPlayer;
  public GameObject shadowPlayer;

  private Bounds lightDoorBounds;
  private Bounds shadowDoorBounds;

  // Use this for initialization
  void Start() {

    // Get the position of door objects
    GameObject lightDoor = GameObject.FindWithTag("LightDoor");
    GameObject shadowDoor = GameObject.FindWithTag("ShadowDoor");
    lightDoorBounds = lightDoor.GetComponent<Collider2D>().bounds;
    shadowDoorBounds = shadowDoor.GetComponent<Collider2D>().bounds;
  }
  
  void LateUpdate() {
    Bounds lightPlayerBounds = lightPlayer.GetComponent<Collider2D>().bounds;
    Bounds shadowPlayerBounds = shadowPlayer.GetComponent<Collider2D>().bounds;
    
    // 
    if (lightDoorBounds.Contains(lightPlayerBounds.min) 
      && lightDoorBounds.Contains(lightPlayerBounds.max)
      && shadowDoorBounds.Contains(shadowPlayerBounds.min) 
      && shadowDoorBounds.Contains(shadowPlayerBounds.max)) {
      SceneManager.LoadScene(nextScene);
    }
  }
}
