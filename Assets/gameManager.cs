using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager
{

    public int Points;
    private bool myGameOver;

    private static gameManager instance = null;

    public static gameManager Instance
    {
        get
        {
            if (gameManager.instance == null)
            {
                gameManager.instance = new gameManager();
            }
            return gameManager.instance;
        }
    }

    public void IncreasePoints(int value){
        this.Points += value;
    }

    internal void Restart()
    {
        GameOver = false;
        Points = 0;
        SceneManager.LoadScene("scene");
    }

    public bool GameOver
    {
        get{
            return myGameOver;
        }

        set
        {
            myGameOver = value;
        }

    }
}
