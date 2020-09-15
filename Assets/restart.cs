using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class restart : MonoBehaviour
{

    public void RestartAction(){
        gameManager.Instance.Restart();
        gameManager.Instance.IsPaused = false;
        Time.timeScale = 1f;
    }

    public void ResumeAction(){
        Time.timeScale = 0.05f;
        Invoke("ResumeFullSpeed", 2 / (1/ Time.timeScale)); // 2 real seconds

        TextMeshProUGUI stageText = GameObject.Find("CenterText").GetComponent<TextMeshProUGUI>();
        stageText.fontSize = 120;
        stageText.text = "Get Ready...";
        stageText.color = new Color(stageText.color.r, stageText.color.g, stageText.color.b, 0.8f);

        //hide pause menu now
        this.gameObject.SetActive(false);
    }

    private void ResumeFullSpeed(){
        Time.timeScale = 1f;
        gameManager.Instance.IsPaused = false; //Allow player to move again
    }
}
