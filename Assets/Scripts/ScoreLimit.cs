using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLimit : MonoBehaviour {

    static int limit = 3;

	public static void ScoreLimitWinCondition (bool isLeftScore) {

        if (isLeftScore)
        {
            string leftScore = GameObject.Find("Left Score").GetComponent<Text>().text;

            if (int.Parse(leftScore) == limit)
            {
                print("The left player won.");
            }
        }

        else
        {
            string rightScore = GameObject.Find("Right Score").GetComponent<Text>().text;

            if (int.Parse(rightScore) == limit)
            {
                print("The right player won.");
            }
        }
	}
}
