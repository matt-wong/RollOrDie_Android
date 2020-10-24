using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    Text myPointKeeper;
    Image mySpicyIcon;
    Text myFinalScoreText;
    Text myHighScoreText;
    playerScript myPlayer;
    Transform myRestartPanel;
    Transform myPausePanel;
    Transform myPauseButton;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        gm = gameManager.Instance;

        this.myPointKeeper = GameObject.Find("scoreText").GetComponent<Text>();
        this.mySpicyIcon = GameObject.Find("spicyIcon").GetComponent<Image>();
        
        if (mySpicyIcon){
            this.mySpicyIcon.color = gm.difficulty == eDifficulty.spicy ? Color.white : Color.clear;
        }

        this.myPlayer = GameObject.FindObjectOfType<playerScript>();

        myRestartPanel = transform.Find("RestartPanel").GetComponent<Transform>();
        this.myFinalScoreText = myRestartPanel.Find("FinalScoreText").GetComponent<Text>();
        this.myHighScoreText = myRestartPanel.Find("HighScoreText").GetComponent<Text>();
        myRestartPanel.gameObject.SetActive(false);

        myPausePanel = transform.Find("PausePanel").GetComponent<Transform>();
        myPauseButton = transform.Find("PauseButton").GetComponent<Transform>();

    }


    // Update is called once per frame
    void Update()
    {

        //TODO: Use events for this instead of every update!
        myPointKeeper.text = "Points: " + gm.Points.ToString();
        //myDebugData.text = "Player Lives:" + myPlayer.ExtraLives;
        if (gm.GameOver)
        {
            myRestartPanel.gameObject.SetActive(true);
            myPauseButton.gameObject.SetActive(false);
            myFinalScoreText.text = "Points: " + gm.Points.ToString();
            myHighScoreText.text = "High Score: " + gm.HighScore.ToString();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        myPausePanel.gameObject.SetActive(true);
        gameManager.Instance.IsPaused = true;

    }

    void OnApplicationPause(bool pauseStatus)
    {
        if (myPausePanel){
            PauseGame();
        }
    }

}
