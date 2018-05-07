using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeLimit : MonoBehaviour {

    private static bool isTimeLimitActive; // Checkbox control
    private Text minutesValue, secondsValue; // Textboxes for time in the menu
    private static TimeSpan limit; // Time
    private Text clock; // In-game clock text
    Text countDown; // In-game countdown text
    [SerializeField] int secondsIncrement = 10;
    

    private void Start()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main Scene"))
        {
            isTimeLimitActive = GameObject.Find("Time Toggle").GetComponent<Toggle>().isOn;
            minutesValue = GameObject.Find("Minutes Value").GetComponent<Text>();
            secondsValue = GameObject.Find("Seconds Value").GetComponent<Text>();

            limit = new TimeSpan(0, 0, 0);

            minutesValue.text = limit.Minutes.ToString();
            secondsValue.text = limit.Seconds.ToString();
        }

        else
        {
            if (limit.TotalSeconds == 0 || !isTimeLimitActive)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }

            else
            {
                clock = GameObject.Find("Time Limit").GetComponent<Text>();
                clock.text = string.Format("{0:00}:{1:00}", limit.Minutes, limit.Seconds);

                countDown = GameObject.Find("Pause Countdown").GetComponent<Text>();

                StartCoroutine(Clock());
            }
        }
    }

    public void Set_isTimeLimitActive() { isTimeLimitActive = !isTimeLimitActive; }

    public void IncrementTimeLimitSeconds()
    {
        limit = limit.Add(new TimeSpan(0, 0, secondsIncrement));

        minutesValue.text = limit.Minutes.ToString();
        secondsValue.text = limit.Seconds.ToString();
    }

    public void IncrementTimeLimitMinutes()
    {
        limit = limit.Add(new TimeSpan(0, 1, 0));

        minutesValue.text = limit.Minutes.ToString();
    }

    public void DecrementTimeLimitSeconds()
    {
        if (limit.TotalSeconds > 0) { limit = limit.Subtract(new TimeSpan(0, 0, secondsIncrement)); }

        minutesValue.text = limit.Minutes.ToString();
        secondsValue.text = limit.Seconds.ToString();
    }

    public void DecrementTimeLimitMinutes()
    {
        if (limit.TotalMinutes > 0) { limit = limit.Subtract(new TimeSpan(0, 1, 0)); }

        minutesValue.text = limit.Minutes.ToString();
    }

    public IEnumerator Clock()
    {
        TimeSpan clockCountdown = limit;

        while(clockCountdown.TotalSeconds > 0)
        {
            yield return new WaitForSeconds(1);

            if (!countDown.enabled) // If the Pause Countdown isn't enabled
            {
                clockCountdown = clockCountdown.Subtract(new TimeSpan(0, 0, 1));
                clock.text = string.Format("{0:00}:{1:00}", clockCountdown.Minutes, clockCountdown.Seconds);
            }
        }

        print("The game is over.");
    }
}
