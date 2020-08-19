using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obstacleScript : fallingObject
{

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            if (!col.gameObject.GetComponent(typeof(playerScript)).Equals(null))
            {
                playerScript playerHitScript = (playerScript)col.gameObject.GetComponent(typeof(playerScript));

                if (playerHitScript.ExtraLives > 0)
                {
                    //Player collected a heart make this enemy die now
                    var effects = GameObject.FindObjectOfType<EffectsMaker>();
                    if (effects){
                        effects.HeartEffect(this.transform.position);
                    }
                    playerHitScript.ExtraLives -= 1;
                    GetKilled();
                }

                else if (!playerHitScript.invincible)
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

    private void GetKilled()
    {
        Animator ani = Camera.main.GetComponent<Animator>();

        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("CameraZoom"))
        {
            ani.Play("CameraShake");
        }

        Destroy(gameObject);
    }

    internal void GetKilled(float v)
    {
       Destroy(gameObject);
    }
}
