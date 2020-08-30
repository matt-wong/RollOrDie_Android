using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{

    private AudioSource myAudioSource;
    public AudioClip[] Songs;
    private stageManager stageManager;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = gameObject.GetComponent<AudioSource>();
        myAudioSource.clip = Songs[UnityEngine.Random.Range(0,this.Songs.Length)];
        myAudioSource.Play();

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.stageManager = canvas.GetComponent<stageManager>();
        if (this.stageManager){
            this.stageManager.NewStageAction += (value) => {this.AdjustToNewStage(value);};
        }
    }

    private void AdjustToNewStage(Stage value)
    {
        if (value.MusicSpeed > 0){ //Was set.
        this.myAudioSource.pitch = value.MusicSpeed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
