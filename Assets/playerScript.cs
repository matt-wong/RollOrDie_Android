using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{

    public int value = 0;
    public bool invincible = false;
    private bool gotLosingRoll = false;

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
            transform.position = new Vector3(System.Math.Min(transform.position.x + 1, 3), transform.position.y, 0);
        }
        else if (vector3.x < (Screen.width * 1 / 3))
        { // Left thrid of Screen
            transform.position = new Vector3(System.Math.Max(transform.position.x - 1, -4), transform.position.y, 0);
        }
        else
        {
            Roll();
        }

    }

    void Roll()
    {
        rb.AddForce(new Vector2(0f, 250));
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
            transform.position = new Vector3(System.Math.Max(transform.position.x - 1, -4), transform.position.y, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position = new Vector3(System.Math.Min(transform.position.x + 1, 3), transform.position.y, 0);
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
        if (col.tag == "Floor")
        {

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
