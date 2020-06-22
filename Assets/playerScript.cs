﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{

    public int value = 0;
    public bool invincible = false;
    private bool gotLosingRoll = false;
    private float lastRollTime;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {

        TextMesh tm = GetComponentInChildren<TextMesh>();
        this.value = Random.Range(1, 7);
        tm.text = this.value.ToString();

        rb = GetComponentInChildren<Rigidbody2D>();
    }

    // Update is called once per frame
    void HandleTouch(Vector3 vector3)
    {

        if (vector3.x > (Screen.width * 2 / 3))
        { //Right Third of Screen
            moveRight();
        }
        else if (vector3.x < (Screen.width * 1 / 3))
        { // Left thrid of Screen
            moveLeft();
        }
        else
        {
            Roll();
        }

    }

    void moveRight()
    {
        transform.position = new Vector3(System.Math.Min(transform.position.x + 1, 3.5f), transform.position.y, 0);
    }

    void moveLeft()
    {
        transform.position = new Vector3(System.Math.Max(transform.position.x - 1, -3.5f), transform.position.y, 0);
    }

    void Roll()
    {
        rb.freezeRotation = false;
        rb.AddForce(new Vector2(0f, 250));
        rb.AddTorque(75);
        this.value = 0; //Die if hit while rolling
    }

    void Update()
    {

        // Handle native touch events
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                this.HandleTouch(touch.position);
            }
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleTouch(Input.mousePosition);
            }
        }

        //Arrow controls for computers
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveRight();
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Roll();
        }

        TextMesh tm = GetComponentInChildren<TextMesh>();
        tm.text = this.value.ToString();

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Floor" && (Time.fixedTime - lastRollTime > 0.25f || lastRollTime == 0))
        {
            lastRollTime = Time.fixedTime;
            rb.freezeRotation = true;
            rb.rotation = 0;

            //Don't allow player to roll losing roll twice in a row
            int lowerRange = 1;
            if (gotLosingRoll)
            {
                lowerRange = gameManager.Instance.weakestEnemyHP + 1;
                gotLosingRoll = false;
            }
            this.value = Random.Range(lowerRange, 7);

            if (this.value <= gameManager.Instance.weakestEnemyHP)
            {
                //take note that we gave them a losing roll
                gotLosingRoll = true;
            }
        }
    }
}
