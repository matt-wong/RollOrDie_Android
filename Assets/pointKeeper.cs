using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pointKeeper : MonoBehaviour
{
    const int MATCHES_FOR_MULTIPLER_BONUS = 4;
    const float PITCH_INCREASE_RATE = 0.5f;

    private int pointMultiplyer = 1;
    private int matchCounter = 0;    
    public AudioClip MatchNoise;
    public AudioClip ResetNoise;

    private AudioSource audSource;
    private Canvas canv;
    public GameObject TextDescriptor;
    // Start is called before the first frame update
    void Start()
    {
        // Makes match noises (increase matches + comboBreaker)
        audSource = gameObject.GetComponent<AudioSource>();
        canv = GameObject.Find("ItemsDescriptionCanvas").GetComponent<Canvas>();
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

        if (this.GetMatchDescription() != "" && canv != null){
            Vector3 descPosition = new Vector3(System.Math.Max(System.Math.Min(this.transform.position.x, 1.8f),-1.8f), this.transform.position.y, this.transform.position.z);
            GameObject newItemDesc = Instantiate(this.TextDescriptor, descPosition, Quaternion.identity, canv.transform); 
            TextMeshProUGUI text = newItemDesc.GetComponent<TextMeshProUGUI>();
            text.text = this.GetMatchDescription();
        }

        Debug.Log("MATCH BONUS:");
        Debug.Log(this.matchCounter);
        Debug.Log(this.pointMultiplyer);

        // As the match count goes up, the sound gets higher! BA-DING!
        audSource.pitch = 1 + PITCH_INCREASE_RATE * (float)matchCounter / (float)MATCHES_FOR_MULTIPLER_BONUS;
        audSource.PlayOneShot(this.MatchNoise);
    }

    private string GetMatchDescription()
    {
        if (matchCounter == 1){
            return "Match!";
        }else if (matchCounter == 2){
            return "Double Match!";
        }else if (matchCounter == 3){
            return "Triple Match!";
        }else if (matchCounter > 3 && matchCounter % MATCHES_FOR_MULTIPLER_BONUS == 0){
            return "Match Bonus X" + pointMultiplyer;
        }else {
            return "";
        }

    }

    internal void ResetMatchMultiplier()
    {
        // TODO: Indication of lost of multiplyer
        if (this.matchCounter > 0 && this.pointMultiplyer > 1)
        {
            //Multiplier lost...
            audSource.PlayOneShot(this.ResetNoise);
            if (canv != null)
            {
                Vector3 descPosition = new Vector3(System.Math.Max(System.Math.Min(this.transform.position.x, 1.8f), -1.8f), this.transform.position.y, this.transform.position.z);
                GameObject newItemDesc = Instantiate(this.TextDescriptor, descPosition, Quaternion.identity, canv.transform);
                TextMeshProUGUI text = newItemDesc.GetComponent<TextMeshProUGUI>();
                text.text = "Match Bonus Lost :(";
            }
        }

        this.matchCounter = 0;
        this.pointMultiplyer = Math.Max(matchCounter / MATCHES_FOR_MULTIPLER_BONUS, 1);
        Debug.Log("LOST MATCH BONUS:");
        Debug.Log(this.matchCounter);
        Debug.Log(this.pointMultiplyer);

        audSource.pitch = 1;

    }
}
