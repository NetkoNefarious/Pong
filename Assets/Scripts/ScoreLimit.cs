using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreLimit : MonoBehaviour {
    private static bool isScoreActive = false;
    public static int limitScore;
    private Text scoreLimitValue;

    private void Start()
    {
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main Scene"))
        {
            isScoreActive = false;
            scoreLimitValue = GameObject.Find("Score Value").GetComponent<Text>();
            scoreLimitValue.text = limitScore.ToString();
        }

        else
        {
            isScoreActive = true;

            if (limitScore == 0)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }
    }

    public void IncrementScoreLimit()
    {
        scoreLimitValue.text = (++limitScore).ToString();
    }

    public void DecrementScoreLimit()
    {
        if (int.Parse(scoreLimitValue.text) > 0)
        {
            scoreLimitValue.text = (--limitScore).ToString();
        }
    }

    public static void ScoreLimitWinCondition (bool isLeftScore) {
        if (limitScore != 0)
        {
            if (isLeftScore)
            {
                string leftScore = GameObject.Find("Left Score").GetComponent<Text>().text;

                if (int.Parse(leftScore) == limitScore)
                {
                    print("The left player won.");
                }
            }

            else
            {
                string rightScore = GameObject.Find("Right Score").GetComponent<Text>().text;

                if (int.Parse(rightScore) == limitScore)
                {
                    print("The right player won.");
                }
            }
        }
	}
}
