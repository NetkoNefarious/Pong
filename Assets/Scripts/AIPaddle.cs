using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour {

    public Ball ball;
    [SerializeField] float speed = 250f;
    private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 direction;

		if (ball.transform.position.y != transform.position.y)
        {
            direction = new Vector2(0, ball.transform.position.y-transform.position.y);
        }

        else
        {
            direction = new Vector2(0, 0);
        }

        rigidBody.velocity = direction * speed * Time.deltaTime;
    }
}
