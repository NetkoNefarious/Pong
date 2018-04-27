using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLimit : MonoBehaviour {
    private static bool isTimeActive = false;
    public static int limitSeconds;
    private Text timeLimitValue;
    private Text timeLimit;
    Text countDown;

    private void Start()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main Scene"))
        {
            isTimeActive = false;
            timeLimitValue = GameObject.Find("Time Value").GetComponent<Text>();
            timeLimitValue.text = limitSeconds.ToString();
        }

        else
        {
            isTimeActive = true;

            if (limitSeconds == 0)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }

            else
            {
                timeLimit = GameObject.Find("Time Limit").GetComponent<Text>();
                timeLimit.text = limitSeconds.ToString();
                countDown = GameObject.Find("Pause Countdown").GetComponent<Text>();
                StartCoroutine(Clock());
            }
        }
    }
    public void IncrementTimeLimit()
    {
        timeLimitValue.text = (++limitSeconds).ToString();
    }

    public void DecrementTimeLimit()
    {
        if (limitSeconds > 0)
        {
            timeLimitValue.text = (--limitSeconds).ToString();
        }
    }

    public IEnumerator Clock()
    {
        int i = limitSeconds;
        while(i > 0)
        {
            yield return new WaitForSeconds(1);

            if (!countDown.enabled) // If the Pause Countdown isn't enabled
            {
                timeLimit.text = (--i).ToString();
            }
        }

        print("The game is over.");
    }
}
