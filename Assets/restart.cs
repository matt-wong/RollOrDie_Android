using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restart : MonoBehaviour
{
    public void RestartAction(){
        gameManager.Instance.Restart();
        gameManager.Instance.IsPaused = false;
        Time.timeScale = 1f;
    }

    public void ResumeAction(){
        Time.timeScale = 1f;
        gameManager.Instance.IsPaused = false;
        //hide pause menu now
        this.gameObject.SetActive(false);
    }
}
