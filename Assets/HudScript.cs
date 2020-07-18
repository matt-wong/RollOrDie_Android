using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    Text myPointKeeper;
    Text myDebugData;
    Text myFinalScoreText;
    Text myHighScoreText;
    playerScript myPlayer;
    Transform myRestartPanel;
    Transform myPausePanel;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
     
        this.myPointKeeper = transform.Find("scoreText").GetComponent<Text>();
        this.myDebugData = transform.Find("debugData").GetComponent<Text>();

        this.myPlayer = GameObject.FindObjectOfType<playerScript>();

        myRestartPanel = transform.Find("RestartPanel").GetComponent<Transform>();
        this.myFinalScoreText = myRestartPanel.Find("FinalScoreText").GetComponent<Text>();
        this.myHighScoreText = myRestartPanel.Find("HighScoreText").GetComponent<Text>();
        myRestartPanel.gameObject.SetActive(false);

        myPausePanel = transform.Find("PausePanel").GetComponent<Transform>();

        gm = gameManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        myPointKeeper.text = "Points: " + gm.Points.ToString();
        myDebugData.text = "Player Lives:" + myPlayer.ExtraLives;
        if (gm.GameOver){
            myRestartPanel.gameObject.SetActive(true);
            myFinalScoreText.text = "Points: " + gm.Points.ToString();
            myHighScoreText.text = "High Score: " + gm.HighScore.ToString();
        }

    }

    public void PauseGame(){
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1f;
            myPausePanel.gameObject.SetActive(false);
        }
        else
        {
            Time.timeScale = 0f;
            myPausePanel.gameObject.SetActive(true);
        }

    }
}
