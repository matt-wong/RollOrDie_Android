using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class optionsScript : MonoBehaviour
{

    Text musicButtonText;
    Text soundButtonText;

    // Start is called before the first frame update
    void Start()
    {
        musicButtonText = GameObject.Find("MusicButtonText").GetComponent<Text>();
        soundButtonText = GameObject.Find("SoundButtonText").GetComponent<Text>();
        this.RefreshSettingsText();
    }

    private void RefreshSettingsText()
    {
        musicButtonText.text = "Music is " + (gameManager.Instance.MuteMusic ? "OFF" : "ON");
        soundButtonText.text = "Sound is " + (gameManager.Instance.MuteSounds ? "OFF" : "ON");        
    }

    public void PressMusicButton(){
        gameManager.Instance.MuteMusic = !gameManager.Instance.MuteMusic;
        RefreshSettingsText();
    }

    public void PressSoundButton(){
        gameManager.Instance.MuteSounds = !gameManager.Instance.MuteSounds;
        RefreshSettingsText();
    }

}
