using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudScript : MonoBehaviour
{

    Text myPointKeeper;
    Button myRestartButton;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        myPointKeeper = GetComponentInChildren<Text>();
        myRestartButton = GetComponentInChildren<Button>();
        myRestartButton.gameObject.SetActive(false);

        gm = gameManager.Instance;
    }


    // Update is called once per frame
    void Update()
    {
        myPointKeeper.text = "Points: " + gm.Points.ToString();
        if (gm.GameOver){
            myRestartButton.gameObject.SetActive(true);
        }
    }
}
