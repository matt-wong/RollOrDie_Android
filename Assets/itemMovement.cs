using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemMovement : MonoBehaviour
{
    public float CenterY = -3.5f;
    public float Amplitude = 1f;
    public float MinX = -3.5f; //Check if the item is below this height -> flap up
    public float MaxX = 3.5f; //Check if the item is below this height -> flap up
    
    public float horizSpeed = 1f;
    //private Rigidbody2D rb;

    private float lastBump = 0f;

    // Start is called before the first frame update
    void Start()
    {
         //this.rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

         Vector3 newPos= transform.position;
        newPos.z = 0;
        newPos.y = Amplitude*Mathf.Sin(Time.time) + CenterY;
        newPos.x += horizSpeed * Time.deltaTime;
        transform.position = newPos;

        // float x = horizSpeed * Time.deltaTime;
        // transform.position = new Vector3(transform.position.x + x, Mathf.Sin(Time.time) + MinY, transform.position.z);
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Bumper" && Time.time - lastBump > 0.05f)
        {
            lastBump = Time.time;
            horizSpeed = horizSpeed * -1;
        }
    }

}
