using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{
    private Color PROGRESS_BAR_COLOR = new Color(0.3019608f, 0.8588235f, 0.4588235f, 1); 

    Text myScoreText;
    Image myMultiBadge;
    Text myMultiText;

    Image[] myMultiProgess;
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

        this.myMultiProgess = new Image[3];
        this.myMultiProgess[0] = GameObject.Find("MultiplierProgress_0").GetComponent<Image>();
        this.myMultiProgess[1] = GameObject.Find("MultiplierProgress_1").GetComponent<Image>();
        this.myMultiProgess[2] = GameObject.Find("MultiplierProgress_2").GetComponent<Image>();

        this.mySpicyIcon = GameObject.Find("spicyIcon").GetComponent<Image>();

        
        
        if (mySpicyIcon){
            this.mySpicyIcon.color = gm.difficulty == eDifficulty.spicy ? Color.white : Color.clear;
        }

        this.myPlayer = GameObject.FindObjectOfType<playerScript>();
        this.myPlayer.PlayerDied += () => {ShowGameOverMenu();};

        this.myPointKeeper = GameObject.FindObjectOfType<pointKeeper>();
        myPointKeeper.UpdateAction += (value) => {UpdateFromPointKeeper(value);};

        myRestartPanel = transform.Find("RestartPanel").GetComponent<Transform>();
        this.myFinalScoreText = myRestartPanel.Find("FinalScoreText").GetComponent<Text>();
        this.myHighScoreText = myRestartPanel.Find("HighScoreText").GetComponent<Text>();
        myRestartPanel.gameObject.SetActive(false);

        myPausePanel = transform.Find("PausePanel").GetComponent<Transform>();
        myPauseButton = transform.Find("PauseButton").GetComponent<Transform>();

    }

    void ShowGameOverMenu(){
        if (gm.GameWasWon){
            SceneManager.LoadScene("Credits");
        }
        
        else if (gm.GameOver)
        {
            myRestartPanel.gameObject.SetActive(true);
            myPauseButton.gameObject.SetActive(false);
            myFinalScoreText.text = "Points: " + gm.Points.ToString();
            myHighScoreText.text = "High Score: " + gm.HighScore.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    void UpdateFromPointKeeper(int value){
        myScoreText.text = "Points: " + value.ToString();

        // Show/Hide multiplier badge
        if (myPointKeeper.PointMultiplier > 1)
        {
            myMultiBadge.color = MATCH_GREEN;
            myMultiText.text = "x" + myPointKeeper.PointMultiplier.ToString();
        }
        else
        {
            myMultiBadge.color = Color.clear;
            myMultiText.text = "";
        }

        int progressBarsVisible = myPointKeeper.MatchCounter % pointKeeper.MATCHES_FOR_MULTIPLER_BONUS;
        foreach(Image img in myMultiProgess){
            img.color = Color.clear;
        }

        for (int i= 0; i < progressBarsVisible; i ++){
            if (!!myMultiProgess[i]){
                myMultiProgess[i].color = PROGRESS_BAR_COLOR;
            }
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
