using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour {
    [SerializeField] float speed = 30;
    private Rigidbody2D rigidBody;
    private AudioSource audioSource;
    private Vector2 direction = new Vector2();

    // Use this for initialization
    void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Left Paddle or Right Paddle
        if (collision.gameObject.name == "Left Paddle" || collision.gameObject.name == "Right Paddle")
        {
            HandlePaddleHit(collision);
        }

        // Bottom Wall or Top Wall
        if (collision.gameObject.name == "Top Wall" || collision.gameObject.name == "Bottom Wall")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.wallBloop);
        }

        // Left Goal or Right Goal
        if (collision.gameObject.name == "Left Goal" || collision.gameObject.name == "Right Goal")
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.goalBloop);

            if (collision.gameObject.name == "Left Goal")
            {
                UpdateAndContinue("Right Score");

                ScoreLimit.ScoreLimitWinCondition(isLeftScore: false);

                // Makes the ball go horizontally straight after scoring (in this case to the left)
                direction = Vector2.left.normalized;
            }

            if (collision.gameObject.name == "Right Goal")
            {
                UpdateAndContinue("Left Score");

                ScoreLimit.ScoreLimitWinCondition(isLeftScore: true);

                // This one is especially important for AI in order to not get scored on repeatedly
                direction = Vector2.right.normalized;
            }

            // Reset ball position
            transform.position = new Vector2(0, 0);
        }
    }

    private void UpdateAndContinue(string goalScore)
    {
        IncreaseTextUIScore(goalScore);

        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown(){
        // References
        Text countDown = GameObject.Find("Pause Countdown").GetComponent<Text>();

        // Settings
        countDown.text = "3";
        rigidBody.velocity = 0 * direction;
        countDown.enabled = true;

        for (int i = 3; i > 0; i--)
        {
            countDown.text = i.ToString();
            yield return new WaitForSeconds(1);
        }

        countDown.enabled = false;
        rigidBody.velocity = speed * direction;
    }

    private void HandlePaddleHit(Collision2D collision)
    {
        float y = BallHitPaddleWhere(transform.position, collision.transform.position, collision.collider.bounds.size.y);

        if (collision.gameObject.name == "Left Paddle")
        {
            direction = new Vector2(1, y).normalized;
        }

        else if (collision.gameObject.name == "Right Paddle")
        {
            direction = new Vector2(-1, y).normalized;
        }

        rigidBody.velocity = speed * direction;

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBloop);
    }

    private float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }

    void IncreaseTextUIScore(string textUIName)
    {
        var textUIComp = GameObject.Find(textUIName).GetComponent<Text>();

        int score = int.Parse(textUIComp.text);
        score++;
        textUIComp.text = score.ToString();
    }
}
