using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameManager
{

    public int Points;
    public int HighScore;
    private bool myGameOver;
    public int weakestEnemyHP;
    public int stageNumber = 0;

    //0 (0 - 20 points) - No items, playter rigged to never roll losing value twice in a row.
    //1 - (20 -X points) - Add dots items to make every row winnable.

    private static gameManager instance = null;

    public static gameManager Instance
    {
        get
        {
            if (gameManager.instance == null)
            {
                gameManager.instance = new gameManager();
                gameManager.instance.Load();
            }
            return gameManager.instance;
        }
    }

    public void IncreasePoints(int value){
        this.Points += value;
        if (Points > HighScore){
            HighScore = Points;
        }
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
            if (myGameOver){
                Save();
            }
        }

    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        bf.Serialize(file, HighScore);
        file.Close();
    }
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            int highscoreLoad = (int)bf.Deserialize(file);
            file.Close();

            HighScore = highscoreLoad;

        }
    }
}
