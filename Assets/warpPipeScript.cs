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

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("TOUCHING");
    }

}
