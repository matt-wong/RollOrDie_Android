using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpPipeScript : MonoBehaviour
{
    playerScript myPlayer;
    SpriteRenderer myLeftPipe;
    SpriteRenderer myRightPipe;

    // Start is called before the first frame update
    void Start()
    {
        this.myPlayer = GameObject.FindObjectOfType<playerScript>();
        this.myLeftPipe = transform.Find("WarpPipeL").GetComponent<SpriteRenderer>();
        this.myRightPipe = transform.Find("WarpPipeR").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPlayer.CanWrap){
            myLeftPipe.color = Color.green;
            myRightPipe.color = Color.green;
        }
    }
}
