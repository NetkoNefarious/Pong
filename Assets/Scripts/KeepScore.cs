using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class KeepScore : MonoBehaviour {

    private static int leftScore, rightScore;
    [SerializeField] int countdownSeconds = 3;
    Ball ball;
    Rigidbody2D ballRigidbody2D;
    Text countDown;


    private void Start()
    {
        leftScore = 0;
        rightScore = 0;

        // References
        ball = GameObject.Find("Ball").GetComponent<Ball>();
        ballRigidbody2D = GameObject.Find("Ball").GetComponent<Rigidbody2D>();
        countDown = GameObject.Find("Pause Countdown").GetComponent<Text>();
    }

    public static void IncreaseTextUIScore(string textUIName)
    {
        var textUIComp = GameObject.Find(textUIName).GetComponent<Text>();

        switch(textUIName)
        {
            case ("Left Score"):
                leftScore++;
                textUIComp.text = leftScore.ToString();
                break;
            case ("Right Score"):
                rightScore++;
                textUIComp.text = rightScore.ToString();
                break;
        }
    }

    // Coroutine
    public IEnumerator Countdown()
    {
        // Settings
        countDown.text = countdownSeconds.ToString();
        ballRigidbody2D.velocity = 0 * ball.Direction;
        countDown.enabled = true;

        for (int i = 3; i > 0; i--)
        {
            countDown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        // Settings
        countDown.enabled = false;
        ballRigidbody2D.velocity = ball.GetSpeed * ball.Direction;
    }
}
