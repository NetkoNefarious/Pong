using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    [SerializeField] int secondsTillSceneLoad;

	// Use this for initialization
	void Start () {
        Invoke("OpenNextScene", secondsTillSceneLoad);
	}

    void OpenNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
