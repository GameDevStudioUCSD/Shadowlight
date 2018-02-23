using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfDemo : MonoBehaviour {

    public string nextScene;

    void Update () {
        Invoke("GoToTitleScreen", 5);
    }

    void GoToTitleScreen() {
        SceneManager.LoadScene(nextScene);
    }
}
