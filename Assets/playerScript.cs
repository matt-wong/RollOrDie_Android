using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{

    public int value = 0;
    public bool invincible = false;
    private bool gotLosingRoll = false;
    private float lastRollTime;

    DiceFace[] faces;
    private DiceFace currFace;
    public Sprite[] faceSprites;
    
    private SpriteRenderer spriteRend;
    private Rigidbody2D rb;
    public GameObject dustMaker;

    // Start is called before the first frame update
    void Start()
    {
        TextMesh tm = GetComponentInChildren<TextMesh>();

        rb = GetComponentInChildren<Rigidbody2D>();

        this.faces = new DiceFace[6];
        for (int i = 0; i < 6; i++)
        {
            this.faces[i] = new DiceFace(i + 1, faceSprites[i]);
        }

        this.currFace = this.faces[Random.Range(0,6)];
        this.value = currFace.Value;
        spriteRend = GetComponent<SpriteRenderer>();
        spriteRend.sprite = this.currFace.sprite;
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
        if (rb.mass < 1.1){
            rb.AddTorque(75);
            //Only spin when at original Mass (1)
        }
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
        else if (Input.GetKeyDown(KeyCode.Z))
        {

            this.invincible = !this.invincible;
            Debug.Log("Player is Invincible: " + this.invincible);
        }

    }

    public void DecrementValue(){
        this.value -= 1;
        this.spriteRend.sprite = faceSprites[this.value - 1];
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Floor" && (Time.fixedTime - lastRollTime > 0.25f || lastRollTime == 0))
        {

            lastRollTime = Time.fixedTime;

            //Effects
            rb.freezeRotation = true;
            rb.rotation = 0;

            Animator ani = Camera.main.GetComponent<Animator>();
            ani.Play("CameraShake");

            GameObject dust = Instantiate<GameObject>(dustMaker, new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z) , Quaternion.identity);
            Destroy(dust, 2); //get rid of the dust in 2 seconds

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

            this.currFace = faces[this.value - 1];
            this.spriteRend.sprite = this.currFace.sprite;
        }
    }
}
