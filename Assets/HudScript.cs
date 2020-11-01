using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    Text myScoreText;
    Image myMultiBadge;
    Text myMultiText;
    Image mySpicyIcon;
    Text myFinalScoreText;
    Text myHighScoreText;

    Transform myRestartPanel;
    Transform myPausePanel;
    Transform myPauseButton;
    
    pointKeeper myPointKeeper;
    playerScript myPlayer;
    gameManager gm;

    private static Color MATCH_GREEN = new Color(0.756f, 0.921f, 0.8f);
    // Start is called before the first frame update
    void Start()
    {
        gm = gameManager.Instance;

        this.myScoreText = GameObject.Find("scoreText").GetComponent<Text>();
        this.myMultiBadge = GameObject.Find("multiplierBadge").GetComponent<Image>();
        this.myMultiText = GameObject.Find("scoreMultiplier").GetComponent<Text>();
        this.mySpicyIcon = GameObject.Find("spicyIcon").GetComponent<Image>();
        
        if (mySpicyIcon){
            this.mySpicyIcon.color = gm.difficulty == eDifficulty.spicy ? Color.white : Color.clear;
        }

        this.myPlayer = GameObject.FindObjectOfType<playerScript>();

        this.myPointKeeper = GameObject.FindObjectOfType<pointKeeper>();
        myPointKeeper.UpdateAction += (value) => {UpdateFromPointKeeper(value);};

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

        //TODO: Hook to gameover event.
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

    void UpdateFromPointKeeper(int value){
        myScoreText.text = "Points: " + value.ToString();

        // Show/Hide multiplier badge
        if (myPointKeeper.pointMultiplier > 1)
        {
            myMultiBadge.color = MATCH_GREEN;
            myMultiText.text = "x" + myPointKeeper.pointMultiplier.ToString();
        }
        else
        {
            myMultiBadge.color = Color.clear;
            myMultiText.text = "";
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
