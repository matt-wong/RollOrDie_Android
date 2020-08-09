using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum eDifficulty{
    easy, //Slower but no unlocks
    regular
}


public class gameManager
{

    public int Points = 0;
    public int HighScore = 0;

    public bool IsPaused = false;

    public eDifficulty difficulty = eDifficulty.easy;

    private bool myGameOver;
    public int weakestEnemyHP;
    private unlockableManager unlockableManager;

    private static gameManager instance = null;

    public static gameManager Instance
    {
        get
        {
            if (gameManager.instance == null)
            {
                gameManager.instance = new gameManager();
                gameManager.instance.unlockableManager = GameObject.FindObjectOfType<unlockableManager>();
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
                CheckUnlockables();
                Save();
            }
        }

    }

    private void CheckUnlockables()
    {
        UnlockData.SumScore += this.Points;
        unlockableManager.MaybeUnlockNext(this.Points);
    }

    public void Save()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

            bf.Serialize(file, new saveFile(this.HighScore, UnlockData.SumScore, UnlockData.UnlockIndex));
            file.Close();
        }
        catch (Exception ex)
        {

        }
    }

    public void Load()
    {
        try
        {
            if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
                saveFile saveFile = (saveFile)bf.Deserialize(file);
                file.Close();

                HighScore = saveFile.Highscore;
                UnlockData.SumScore = saveFile.cumulativeScore;
                UnlockData.UnlockIndex = saveFile.UnlockIndex;

                Debug.Log("Loaded...");
                Debug.Log("HighScore " + HighScore.ToString());
                Debug.Log("CumulativeScore " + UnlockData.SumScore.ToString());
                Debug.Log("UnlockIndex " + UnlockData.UnlockIndex.ToString());


            }
        }catch (Exception e)
        {
            Debug.Log(e);

            HighScore = 0;
            UnlockData.SumScore = 0;
            UnlockData.UnlockIndex = 0;

        }


    }

}
