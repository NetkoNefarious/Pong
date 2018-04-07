using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            //TO DO : Update Score UI

            transform.position = new Vector2(0, 0);
        }

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

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBloop);
    }

    private float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
    {
        return (ball.y - paddle.y) / paddleHeight;
    }
}
