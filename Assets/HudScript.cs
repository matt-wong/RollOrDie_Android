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
    Text myUnlockText;
    playerScript myPlayer;
    Transform myRestartPanel;
    Transform myPausePanel;
    Transform myPauseButton;

    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
     
        this.myPointKeeper = transform.Find("scoreText").GetComponent<Text>();
        this.myDebugData = transform.Find("debugData").GetComponent<Text>();
        myDebugData.text = "";

        this.myPlayer = GameObject.FindObjectOfType<playerScript>();

        myRestartPanel = transform.Find("RestartPanel").GetComponent<Transform>();
        this.myFinalScoreText = myRestartPanel.Find("FinalScoreText").GetComponent<Text>();
        this.myHighScoreText = myRestartPanel.Find("HighScoreText").GetComponent<Text>();
        this.myUnlockText = myRestartPanel.Find("UnlockText").GetComponent<Text>();
        myRestartPanel.gameObject.SetActive(false);

        myPausePanel = transform.Find("PausePanel").GetComponent<Transform>();
        myPauseButton = transform.Find("PauseButton").GetComponent<Transform>();

        gm = gameManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        myPointKeeper.text = "Points: " + gm.Points.ToString();
        //myDebugData.text = "Player Lives:" + myPlayer.ExtraLives;
        if (gm.GameOver){
            myRestartPanel.gameObject.SetActive(true);
            myPauseButton.gameObject.SetActive(false);
            myFinalScoreText.text = "Points: " + gm.Points.ToString();
            myHighScoreText.text = "High Score: " + gm.HighScore.ToString();
            myUnlockText.text = UnlockData.Status;
        }

    }

    public void PauseGame(){
        {
            Time.timeScale = 0f;
            myPausePanel.gameObject.SetActive(true);
            gameManager.Instance.IsPaused = true;
        }

    }
}
