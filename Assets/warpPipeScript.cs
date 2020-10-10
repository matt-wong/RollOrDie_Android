using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpPipeScript : MonoBehaviour
{
    playerScript myPlayer;
    SpriteRenderer myLeftPipe;
    SpriteRenderer myRightPipe;

    bool isTouchingPlayer = false; 

    // Start is called before the first frame update
    void Start()
    {
        this.myPlayer = GameObject.FindObjectOfType<playerScript>();
        myPlayer.GotUpgradeAction += (value) => {ShowWarpIndicators();};
        this.myLeftPipe = transform.Find("WarpPipeL").GetComponent<SpriteRenderer>();
        this.myRightPipe = transform.Find("WarpPipeR").GetComponent<SpriteRenderer>();
    }

    private void ShowWarpIndicators(){
        if (myPlayer.CanWrap){
            myLeftPipe.color = Color.white;
            myRightPipe.color = Color.white;
        }else{
            myLeftPipe.color = Color.clear;
            myRightPipe.color = Color.clear;
        }
    }

    void Update()
    {
        if (isTouchingPlayer)
        {
            float nextX = Mathf.Lerp(myLeftPipe.transform.localScale.x, 1.5f, Time.deltaTime * 3);
            float nextY = Mathf.Lerp(myLeftPipe.transform.localScale.y, 1.7f, Time.deltaTime * 3);
            myLeftPipe.transform.localScale = new Vector3(nextX, nextY, 1);
            myRightPipe.transform.localScale = new Vector3(nextX, nextY, 1);
        }
        else
        {
            float nextX = Mathf.Lerp(myLeftPipe.transform.localScale.x, 1f, Time.deltaTime * 3);
            float nextY = Mathf.Lerp(myLeftPipe.transform.localScale.y, 1.2f, Time.deltaTime * 3);
            myLeftPipe.transform.localScale = new Vector3(nextX, nextY, 1);
            myRightPipe.transform.localScale = new Vector3(nextX, nextY, 1);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player"){
            isTouchingPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "Player"){
            isTouchingPlayer = false;
        }
    }

}
