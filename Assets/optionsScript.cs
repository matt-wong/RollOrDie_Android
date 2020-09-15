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
      //  this.RefreshSettingsText();
    }

    // private void RefreshSettingsText()
    // {
    //     musicButtonText.text = "Music is " + (gameManager.Instance.MuteAudio ? "OFF" : "ON");
    //     soundButtonText.text = "Sound is " + (gameManager.Instance.MuteSounds ? "OFF" : "ON");        
    // }

}
