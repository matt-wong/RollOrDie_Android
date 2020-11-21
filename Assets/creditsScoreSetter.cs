using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditsScoreSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Text scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        scoreText.text = "Score: " + gameManager.Instance.Points;

        Text highScoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        highScoreText.text = "High Score: " + gameManager.Instance.HighScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
