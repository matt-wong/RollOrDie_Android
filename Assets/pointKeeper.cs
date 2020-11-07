using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class pointKeeper : MonoBehaviour
{
    public const int MATCHES_FOR_MULTIPLER_BONUS = 4;
    const float PITCH_INCREASE_RATE = 0.5f;
    const int MAX_MULTIPLIER = 5;
    public int PointMultiplier = 1;
    public int MatchCounter = 0;    
    public AudioClip MatchNoise;
    public AudioClip ResetNoise;

    private AudioSource audSource;
    private Canvas canv;
    public GameObject TextDescriptor;

    public event System.Action<int> UpdateAction;
    // Start is called before the first frame update
    void Start()
    {
        // Makes match noises (increase matches + comboBreaker)
        audSource = gameObject.GetComponent<AudioSource>();
        canv = GameObject.Find("ItemsDescriptionCanvas").GetComponent<Canvas>();
    }

    internal void IncreasePoints(int baseValue)
    {
        gameManager.Instance.IncreasePoints(baseValue * this.PointMultiplier);
        UpdateAction.Invoke(gameManager.Instance.Points);
    }
    
    internal void IncreaseMatchCounter()
    {
        // Multiplier only goes up to 5x
        this.MatchCounter = Math.Min(this.MatchCounter + 1, (MAX_MULTIPLIER - 1) * MATCHES_FOR_MULTIPLER_BONUS);
        this.PointMultiplier = Math.Max((MatchCounter / MATCHES_FOR_MULTIPLER_BONUS) + 1, 1);

        if (this.GetMatchDescription() != "" && canv != null){
            Vector3 descPosition = new Vector3(System.Math.Max(System.Math.Min(this.transform.position.x, 1.8f),-1.8f), this.transform.position.y, this.transform.position.z);
            GameObject newItemDesc = Instantiate(this.TextDescriptor, descPosition, Quaternion.identity, canv.transform); 
            TextMeshProUGUI text = newItemDesc.GetComponent<TextMeshProUGUI>();
            text.text = this.GetMatchDescription();
        }

        Debug.Log("MATCH BONUS:");
        Debug.Log(this.MatchCounter);
        Debug.Log(this.PointMultiplier);

        // As the match count goes up, the sound gets higher! BA-DING!
        audSource.pitch = 1 + PITCH_INCREASE_RATE * (float)MatchCounter / (float)MATCHES_FOR_MULTIPLER_BONUS;
        audSource.PlayOneShot(this.MatchNoise);

        UpdateAction.Invoke(gameManager.Instance.Points);
    }

    private string GetMatchDescription()
    {
        if (MatchCounter == 1){
            return "Match!";
        }else if (MatchCounter == 2){
            return "Double Match!";
        }else if (MatchCounter == 3){
            return "Triple Match!";
        }else if (MatchCounter > 3){
            return "Match Bonus X" + PointMultiplier;
        }else {
            return "";
        }

    }

    internal void DecreaseMatchMultiplier()
    {
        // Indication of lost of multiplier
        if (this.MatchCounter > 0 && this.PointMultiplier > 1)
        {
            // Multiplier lost...
            audSource.pitch = 1;
            audSource.PlayOneShot(this.ResetNoise);
        }

        this.PointMultiplier = Math.Max(this.PointMultiplier - 1, 1);
        this.MatchCounter = MATCHES_FOR_MULTIPLER_BONUS * (this.PointMultiplier - 1);

        if (canv != null && this.PointMultiplier > 1) // Don't bother showing anything if the multipiler was reduce to 1
        {
            Vector3 descPosition = new Vector3(System.Math.Max(System.Math.Min(this.transform.position.x, 1.8f), -1.8f), this.transform.position.y, this.transform.position.z);
            GameObject newItemDesc = Instantiate(this.TextDescriptor, descPosition, Quaternion.identity, canv.transform);
            TextMeshProUGUI text = newItemDesc.GetComponent<TextMeshProUGUI>();
            text.text = "Match Bonus X" + PointMultiplier; ;
        }

        UpdateAction.Invoke(gameManager.Instance.Points);

    }
}
