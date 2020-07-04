using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    Text myPointKeeper;
    Text myHSKeeper;
    Text myDebugData;
    playerScript myPlayer;
    Transform myRestartPanel;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
     
        this.myHSKeeper = transform.Find("highscoreText").GetComponent<Text>();
        this.myPointKeeper = transform.Find("scoreText").GetComponent<Text>();
        this.myDebugData = transform.Find("debugData").GetComponent<Text>();
        this.myPlayer = GameObject.FindObjectOfType<playerScript>();

        myRestartPanel = transform.Find("RestartPanel").GetComponent<Transform>();
        myRestartPanel.gameObject.SetActive(false);

        gm = gameManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        myPointKeeper.text = "Points: " + gm.Points.ToString();
        myHSKeeper.text = "High Score: " + gm.HighScore.ToString();
        myDebugData.text = "Player Lives:" + myPlayer.ExtraLives;
        if (gm.GameOver){
            myRestartPanel.gameObject.SetActive(true);
        }

    }
}
