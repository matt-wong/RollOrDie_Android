using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{

    private AudioSource myAudioSource;
    public AudioClip[] Songs;
    private float myDafaultMusicVol;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = gameObject.GetComponent<AudioSource>();
        myDafaultMusicVol = myAudioSource.volume;
        myAudioSource.clip = Songs[UnityEngine.Random.Range(0,this.Songs.Length)];
        myAudioSource.Play();

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        stageManager stageManager = canvas.GetComponent<stageManager>();
        if (stageManager){
            stageManager.NewStageAction += (value) => {this.AdjustToNewStage(value);};
            stageManager.GameWon += () => {this.SoftenMusic();};
        }
    }

    void Update(){
        if (myAudioSource.volume < myDafaultMusicVol){
            myAudioSource.volume += Time.deltaTime * 0.1f;
        }
    }

    private void AdjustToNewStage(Stage value)
    {
        if (value.MusicSpeed > 0){ //Was set.
        this.myAudioSource.pitch = value.MusicSpeed;
        }
    }

    private void SoftenMusic(){
        myAudioSource.volume = 0;
    }
}
