using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{

    private AudioSource myAudioSource;
    public AudioClip[] Songs;
    public AudioClip[] SongLoops;
    private stageManager stageManager;
    private int mySongIndex = 0; 

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = gameObject.GetComponent<AudioSource>();
        mySongIndex = UnityEngine.Random.Range(0,this.Songs.Length);
        myAudioSource.clip = Songs[mySongIndex];
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
        if (!myAudioSource.isPlaying)
        {
            myAudioSource.clip = SongLoops[mySongIndex];
            myAudioSource.Play();
            myAudioSource.loop = true;
        }
    }
}
