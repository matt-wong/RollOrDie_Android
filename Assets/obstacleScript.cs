using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obstacleScript : fallingObject
{

    public ParticleSystem DeathParticles;
    
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
                    playerHitScript.GetKilled();
                }

                playerHitScript.ResetMatchMultiplier();
            }
        }
        else if(col.tag == "EnemyManager"){
            //End of the page, die now
            Destroy(gameObject);
        }
    }

    void Awake(){
        Animator animator = GetComponent<Animator>();
        animator.Play("ObstacleAnim", -1, UnityEngine.Random.Range(0f, 1f));

         SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        //Stagger the sortingOrder so touching obstacles don't flicker
         spriteRend.sortingOrder = UnityEngine.Random.Range(0, 100);
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

    internal void GetKilled(float angle)
    {
        
        ParticleSystem ps = Instantiate(DeathParticles, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.Euler(0f, 0f, angle + 40));
        //ps.textureSheetAnimation.SetSprite(0, this.currFace.sprite);
        
        ParticleSystem.MainModule settings = ps.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(new Color(0.462f, 0.058f, 0.058f));

        ps.Play();
       Destroy(gameObject);
    }
}
