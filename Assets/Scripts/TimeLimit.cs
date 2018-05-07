using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class TimeLimit : MonoBehaviour {
    private static TimeSpan limit;
    private static bool isTimeLimitActive;
    public static int limitSeconds, limitMinutes;
    private Text timeLimitValue;
    private Text timeLimit;
    Text countDown;

    private void Start()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main Scene"))
        {
            isTimeLimitActive = GameObject.Find("Time Toggle").GetComponent<Toggle>().isOn;
            timeLimitValue = GameObject.Find("Time Value").GetComponent<Text>();
            timeLimitValue.text = limitSeconds.ToString();
        }

        else
        {
            if (limitSeconds == 0 || !isTimeLimitActive)
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

    public void Set_isTimeLimitActive() { isTimeLimitActive = !isTimeLimitActive; }

    public void IncrementTimeLimitSeconds()
    {
        limitSeconds += 10; // 10 second increment

        if (limitSeconds >= 60)
        {
            limitMinutes++;
            limitSeconds = limitSeconds - 60;
        }

        timeLimitValue.text = limitSeconds.ToString();
    }

    public void IncrementTimeLimitMinutes() { timeLimitValue.text = (++limitMinutes).ToString(); }

    public void DecrementTimeLimitSeconds()
    {
        // 10 second decrement
        if (limitMinutes > 0 || limitSeconds > 0) { limitSeconds -= 10; }

        if (limitSeconds < 0)
        {
            limitSeconds = 60 + limitSeconds;
            limitMinutes--;
        }

        timeLimitValue.text = limitSeconds.ToString();
    }

    public void DecrementTimeLimitMinutes()
    {
        if (limitMinutes > 0)
        {
            limitMinutes--;
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
