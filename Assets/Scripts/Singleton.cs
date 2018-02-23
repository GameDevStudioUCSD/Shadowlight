using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Singleton : MonoBehaviour
{
    public string startingScene;
    public string endingScene;
    private static Singleton instance = null;

    // Use this for initialization
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    void OnEnable()
    { 
        Debug.Log("scene changed");
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == endingScene)
        {
            instance.gameObject.GetComponent<AudioSource>().Stop();
        }

        if (scene.name == startingScene)
        {
            instance.gameObject.GetComponent<AudioSource>().Play();
        }
    }
}
