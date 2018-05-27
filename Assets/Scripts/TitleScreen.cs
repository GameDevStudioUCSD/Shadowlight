using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour {

    public string nextScene;

    void Update()
    {
        if (Input.GetButtonDown("Next")) SceneManager.LoadScene(nextScene);
    }
}
