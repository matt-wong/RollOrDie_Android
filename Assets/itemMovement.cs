using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class itemMovement : MonoBehaviour
{
    public float CenterY = -3.5f;
    public float Amplitude = 1f;
    public GameObject TextDescriptor;
    public string TextDescription;

    protected bool hasBeenCollected = false; 
    public float horizSpeed = 1f;
    //private Rigidbody2D rb;

    private float lastBump = 0f;
    protected float myStartingTime = 0f;
    public float timeToLive = -1f;
    public float fadeTime = 5f;
    public string fadeAnimationName;
    private bool isFading = false;

    protected Animator myAnimator;
    public AudioClip[] PickUpNoises;

    // Start is called before the first frame update
    void Start()
    {
        this.myAnimator = GetComponent<Animator>();
        myStartingTime = Time.time;

        // Half the time, reverse the direction 
        if (Random.Range(0, 2) == 0)
        {
            horizSpeed = -horizSpeed;
        }

         SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        //Stagger the sortingOrder so touching obstacles don't flicker
         spriteRend.sortingOrder = UnityEngine.Random.Range(0, 100);
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!this.hasBeenCollected)
        {
            Vector3 newPos = transform.position;
            newPos.z = 0;
            newPos.y = Amplitude * Mathf.Cos(Time.time - myStartingTime) + CenterY;
            newPos.x += horizSpeed * Time.deltaTime;
            transform.position = newPos;
        }

        if (this.timeToLive > 0f && Time.time - myStartingTime > this.timeToLive)
        {
            Destroy(gameObject);
        }
        else if (this.timeToLive > 0f && Time.time - myStartingTime > (this.timeToLive - this.fadeTime)){
            if (!this.isFading)
            {
                {
                    this.myAnimator.Play(this.fadeAnimationName);
                    this.isFading = true;
                }
            }
        }
    }

    protected abstract void TouchedPlayer(Collider2D col);

    protected void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Bumper" && Time.time - lastBump > 0.05f)
        {
            lastBump = Time.time;
            horizSpeed = horizSpeed * -1;
        }
        else if(col.tag == "Player" && !this.hasBeenCollected){
            this.TouchedPlayer(col);
            this.ShowTextDescription();
        }
    }

    protected void ShowTextDescription(){

        Canvas canv = GameObject.Find("ItemsDescriptionCanvas").GetComponent<Canvas>();
        if (canv != null){
            Vector3 descPosition = new Vector3(System.Math.Max(System.Math.Min(this.transform.position.x, 1.8f),-1.8f), this.transform.position.y, this.transform.position.z);
            GameObject newItemDesc = Instantiate(this.TextDescriptor, descPosition, Quaternion.identity, canv.transform); 
            TextMeshProUGUI text = newItemDesc.GetComponent<TextMeshProUGUI>();
            text.text = this.TextDescription;
        }
    }

}
