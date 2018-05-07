using UnityEngine;

public class Ball : MonoBehaviour {
    // Attributes
    [SerializeField] float speed;
    public Vector2 Direction { get; set; }

    // References
    private Rigidbody2D rigidBody;
    private AudioSource audioSource;
    private KeepScore score;
    private float topWallY, bottomWallY;

    // Use this for initialization
    void Start () {
        Direction = new Vector2();
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * speed;

        score = GameObject.Find("ScoreTimeCanvas").GetComponent<KeepScore>();
        topWallY = GameObject.Find("Top Wall").transform.position.y;
        bottomWallY = GameObject.Find("Bottom Wall").transform.position.y;
    }

    void Update()
    {
        // If the ball goes out of bounds
        if (rigidBody.position.y > topWallY || rigidBody.position.y < bottomWallY)
        {
            print("Out of Bounds");
            UpdateAndContinue(goalScore: null, isOutOfBounds: true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Paddles
        if (collision.gameObject.name.Contains("Paddle"))
        {
            HandlePaddleHit(collision);
        }

        // Walls
        if (collision.gameObject.name.Contains("Wall"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.wallBloop);
        }

        // Goals
        if (collision.gameObject.name.Contains("Goal"))
        {
            SoundManager.Instance.PlayOneShot(SoundManager.Instance.goalBloop);

            if (collision.gameObject.name == "Left Goal")
            {
                UpdateAndContinue("Right Score", isOutOfBounds: false);

                ScoreLimit.ScoreLimitWinCondition(isLeftScore: false);
            }

            if (collision.gameObject.name == "Right Goal")
            {
                UpdateAndContinue("Left Score", isOutOfBounds: false);

                ScoreLimit.ScoreLimitWinCondition(isLeftScore: true);
            }
        }
    }

    private void UpdateAndContinue(string goalScore, bool isOutOfBounds)
    {
        if (!isOutOfBounds) { KeepScore.IncreaseTextUIScore(goalScore); }

        switch (Direction.x > 0)
        {
            case true:
                // This one is especially important for AI in order to not get scored on repeatedly
                Direction = Vector2.right.normalized;
                break;
            default:
                // Makes the ball go horizontally straight after scoring (in this case to the left)
                Direction = Vector2.left.normalized;
                break;
        }

        // Reset ball position
        transform.position = new Vector2(0, 0);

        // Staart the countdown
        StartCoroutine(score.Countdown());
    }

    private void HandlePaddleHit(Collision2D collision)
    {
        float y = BallHitPaddleWhere(transform.position, collision.transform.position, collision.collider.bounds.size.y);

        if (collision.gameObject.name == "Left Paddle")
        {
            Direction = new Vector2(1, y).normalized;
        }

        else if (collision.gameObject.name == "Right Paddle")
        {
            Direction = new Vector2(-1, y).normalized;
        }

        rigidBody.velocity = speed * Direction;

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBloop);
    }

    private float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }

    public float GetSpeed { get { return speed; } }
}
