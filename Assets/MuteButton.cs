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

        if (gameManager.Instance.MuteAudio)
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

    public void ToggleSound()
    {
        gameManager.Instance.MuteAudio = !gameManager.Instance.MuteAudio;
        Debug.Log("Mute is " + gameManager.Instance.MuteAudio);
        if (gameManager.Instance.MuteAudio)
        {
            myButtonText.text = "Sound is OFF";
        }
        else
        {
            myButtonText.text = "Sound is ON";
        }
    }
}
