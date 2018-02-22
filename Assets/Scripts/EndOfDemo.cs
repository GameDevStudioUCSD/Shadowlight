using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDemo : MonoBehaviour {

    public string nextScene;

    void Update () {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene(nextScene);
    }
}
