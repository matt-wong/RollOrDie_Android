using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicScript : MonoBehaviour
{

    private AudioSource myAudioSource;
    public AudioClip[] Songs;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = gameObject.GetComponent<AudioSource>();
        myAudioSource.clip = Songs[UnityEngine.Random.Range(0,this.Songs.Length)];
        myAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
