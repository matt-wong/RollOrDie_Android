using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour
{

    public float speed = 0f;
    public DiceFace currFace;
    gameManager gm;
    DiceFace[] faces;

    public Sprite[] faceSprites;

    void Awake(){
        this.faces = new DiceFace[6];
        this.faces[0] = new DiceFace(1, faceSprites[0]);
        this.faces[1] = new DiceFace(2, faceSprites[1]);
        this.faces[2] = new DiceFace(3, faceSprites[2]);
        this.faces[3] = new DiceFace(4, faceSprites[3]);
        this.faces[4] = new DiceFace(5, faceSprites[4]);
        this.faces[5] = new DiceFace(6, faceSprites[5]);

        this.currFace = this.faces[Random.Range(0,6)];
    }

    // Start is called before the first frame update
    void Start()
    {
        // TextMesh tm = GetComponentInChildren<TextMesh>();
        // tm.text = this.currFace.Value.ToString();
        
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        Debug.Log(spr);
        spr.sprite = this.currFace.sprite;

        gm = gameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos= transform.position;
        newPos.z = 0;
        newPos.y -= speed * Time.deltaTime;
        transform.position = newPos;
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            if (!col.gameObject.GetComponent(typeof(playerScript)).Equals(null))
            {
                playerScript playerHitScript = (playerScript)col.gameObject.GetComponent(typeof(playerScript));

                if (playerHitScript.value > this.currFace.Value)
                {
                    //Decrease the players HP so they cannot stay still all day
                    playerHitScript.value -= 1;
                    gm.IncreasePoints(1);
                    Destroy(gameObject);
                }
                else if(!playerHitScript.invincible)
                {
                    Destroy(col.gameObject);
                    gm.GameOver = true;
                }
            }
        }
        else if(col.tag == "EnemyManager"){
            //End of the page, die now
            Destroy(gameObject);
        }
    }

}
