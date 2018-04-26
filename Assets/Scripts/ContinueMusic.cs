using UnityEngine;

public class ContinueMusic : MonoBehaviour {

    // Flag
    private static bool isMusicPlaying = false;

    // Use this for initialization
    void Start () {

        // If the music is not playing
        if (!isMusicPlaying)
        {
            isMusicPlaying = true; // Set to true to signify that the music is now playing
        }

        else
        {
            Destroy(gameObject); // Destroy the newly-created Music object
        }
    }
	
	// Update is called once per frame
	void Update () {
        DontDestroyOnLoad(gameObject);
	}
}
