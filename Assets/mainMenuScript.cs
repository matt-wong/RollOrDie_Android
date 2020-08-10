using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMenuScript : MonoBehaviour
{

    const int GAME_SCENE = 1;
    const int DIFF_SCENE = 2;

    private void PlayGame(eDifficulty difficulty){
        Debug.Log("PLAY GAME");

        gameManager.Instance.Restart();
        gameManager.Instance.IsPaused = false;
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene(GAME_SCENE);
        gameManager.Instance.difficulty = difficulty;
    }

    public void PlayEasy(){
        this.PlayGame(eDifficulty.regular);
    }

    public void PlayRegular(){
        this.PlayGame(eDifficulty.spicy);
    }

    public void ChooseDifficulty(){
        Debug.Log("PLAY GAME");
        UnityEngine.SceneManagement.SceneManager.LoadScene(DIFF_SCENE);
    }
}
