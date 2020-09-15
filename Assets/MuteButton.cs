using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MuteButton : MonoBehaviour
{

    private TextMeshProUGUI myButtonText;

    void Start()
    {
      myButtonText = transform.GetComponentInChildren<TextMeshProUGUI>();
      Debug.Log(myButtonText);
    }

    public void ToggleSound()
    {
        gameManager.Instance.MuteMusic = !gameManager.Instance.MuteMusic;
        Debug.Log("Mute is " + gameManager.Instance.MuteMusic);
        if (gameManager.Instance.MuteMusic)
        {
            AudioListener.volume = 0;
            myButtonText.text = "Sound is OFF";
        }
        else
        {
            AudioListener.volume = 1;
            myButtonText.text = "Sound is ON";
        }
    }
}
