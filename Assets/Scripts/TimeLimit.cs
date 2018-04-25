using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimeLimit : MonoBehaviour {

    [SerializeField] int limitSeconds;

    private void Start()
    {
        StartCoroutine(Clock());
    }

    public void LimitSeconds_Set(int seconds)
    {
        limitSeconds = seconds;
    }

    public IEnumerator Clock()
    {
        Text countDown = GameObject.Find("Pause Countdown").GetComponent<Text>(); // Reference to the Pause Countdown object

        while(limitSeconds > 0)
        {
            yield return new WaitForSeconds(1);

            if (!countDown.enabled) // If the Pause Countdown is enabled
            {
                limitSeconds--;
                GetComponent<Text>().text = limitSeconds.ToString();
            }
        }

        print("The game is over.");
    }
}
