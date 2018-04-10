using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour {

    public Ball ball;
    [SerializeField] float speed = 30;
    public float lerpTweak = 2f;
    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (ball.transform.position.y > transform.position.y)
        {
            Vector2 direction = new Vector2(0, 1).normalized; // Pomakni AI paddle dolje, normalizira vektor
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, direction * speed, lerpTweak * Time.deltaTime); // This smoothes out the movement
        }

        else if (ball.transform.position.y < transform.position.y)
        {
            Vector2 direction = new Vector2(0, -1).normalized;
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, direction * speed, lerpTweak * Time.deltaTime);
        }

        else
        {
            Vector2 direction = new Vector2(0, 0).normalized;
            rigidBody.velocity = Vector2.Lerp(rigidBody.velocity, direction * speed, lerpTweak * Time.deltaTime);
        }
    }
}
