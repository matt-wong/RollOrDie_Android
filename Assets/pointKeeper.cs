using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointKeeper : MonoBehaviour
{

    const int MATCHES_FOR_MULTIPLER_BONUS = 3;
    const float PITCH_INCREASE_RATE = 0.5f;

    private int pointMultiplyer = 1;
    private int matchCounter = 0;
    public AudioClip MatchNoise;
    public AudioClip ResetNoise;

    private AudioSource audSource;
    // Start is called before the first frame update
    void Start()
    {
        // Makes match noises (increase matches + comboBreaker)
        audSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void IncreasePoints(int baseValue)
    {
        gameManager.Instance.IncreasePoints(baseValue * this.pointMultiplyer);
    }
    
    internal void IncreaseMatchCounter()
    {
        // TODO: Show mult bonus in game
        this.matchCounter += 1;
        this.pointMultiplyer = Math.Max((matchCounter / MATCHES_FOR_MULTIPLER_BONUS) + 1, 1);
        Debug.Log("MATCH BONUS:");
        Debug.Log(this.matchCounter);
        Debug.Log(this.pointMultiplyer);

        // As the match count goes up, the sound gets higher! BA-DING!
        audSource.pitch = 1 + PITCH_INCREASE_RATE * (float)matchCounter / (float)MATCHES_FOR_MULTIPLER_BONUS;
        audSource.PlayOneShot(this.MatchNoise);
    }

    internal void ResetMatchMultiplier()
    {
        // TODO: Indication of lost of multiplyer
        if (this.matchCounter > 0)
        {
            audSource.PlayOneShot(this.ResetNoise);
        }

        this.matchCounter = 0;
        this.pointMultiplyer = Math.Max(matchCounter / MATCHES_FOR_MULTIPLER_BONUS, 1);
        Debug.Log("LOST MATCH BONUS:");
        Debug.Log(this.matchCounter);
        Debug.Log(this.pointMultiplyer);

        audSource.pitch = 1;

    }
}
