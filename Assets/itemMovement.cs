using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class itemMovement : MonoBehaviour
{
    public float CenterY = -3.5f;
    public float Amplitude = 1f;
    public float MinX = -3.5f; //Check if the item is below this height -> flap up
    public float MaxX = 3.5f; //Check if the item is below this height -> flap up
    
    public float horizSpeed = 1f;
    //private Rigidbody2D rb;

    private float lastBump = 0f;
    private float myStartingTime = 0f;
    public float timeToLive = -1f;

    // Start is called before the first frame update
    void Start()
    {
         //this.rb = GetComponent<Rigidbody2D>();
        myStartingTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

         Vector3 newPos= transform.position;
        newPos.z = 0;
        newPos.y = Amplitude*Mathf.Cos(Time.time - myStartingTime) + CenterY;
        newPos.x += horizSpeed * Time.deltaTime;
        transform.position = newPos;

        if (this.timeToLive > 0f && Time.time - myStartingTime > this.timeToLive)
        {
            Destroy(gameObject);
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
        else if(col.tag == "Player"){
            this.TouchedPlayer(col);
        }
    }

}
