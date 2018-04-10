using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    [SerializeField] string sceneToLoad;
    [SerializeField] int secondsTillSceneLoad;

	// Use this for initialization
	void Start () {
        Invoke("OpenNextScene", secondsTillSceneLoad);
	}

    void OpenNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
