using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    Text myPointKeeper;
    Text myHSKeeper;
    Button myRestartButton;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        Text[] UItexts = GetComponentsInChildren<Text>();

        foreach (Text uit in UItexts){
            if (uit.name == "highscoreText"){
                myHSKeeper = uit;
            }else if(uit.name == "scoreText"){
                myPointKeeper = uit;
            }
        }

        myRestartButton = GetComponentInChildren<Button>();
        myRestartButton.gameObject.SetActive(false);

        gm = gameManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        myPointKeeper.text = "Points: " + gm.Points.ToString();
        myHSKeeper.text = "Points: " + gm.HighScore.ToString();
        if (gm.GameOver){
            myRestartButton.gameObject.SetActive(true);
        }
    }
}
