using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obstacleScript : fallingObject
{

    public ParticleSystem DeathParticles;
    private pointKeeper myPointKeeper;

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            if (!col.gameObject.GetComponent(typeof(playerScript)).Equals(null))
            {
                playerScript playerHitScript = (playerScript)col.gameObject.GetComponent(typeof(playerScript));

                if (playerHitScript.Invincible){
                    GetKilled();
                    return;
                }

                else if (playerHitScript.ExtraLives > 0)
                {
                    //Player collected a heart make this enemy die now
                    var effects = GameObject.FindObjectOfType<EffectsMaker>();
                    if (effects){
                        effects.HeartEffect(this.transform.position);
                    }
                    playerHitScript.TakeDamage();
                    GetKilled();
                    
                    myPointKeeper.DecreaseMatchMultiplier();
 
                }

                else{
                    playerHitScript.GetKilled();
                }

            }
        }
        else if(col.tag == "EnemyManager"){
            //End of the page, die now
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        Animator animator = GetComponent<Animator>();
        if (gameManager.Instance.Points < stageManager.LAST_SECTION_START_INDEX)
        { // Normal - do not spin
            animator.Play("ObstacleAnim", -1, UnityEngine.Random.Range(0f, 1f));
            animator.speed = 0.5f;
        }
        else
        { // Its Geting CRaZY - spin away boys!
            animator.Play("ObstacleSpinAnim", -1, UnityEngine.Random.Range(0f, 1f));
            animator.speed = 1f;
        }


        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        //Stagger the sortingOrder so touching obstacles don't flicker
        spriteRend.sortingOrder = UnityEngine.Random.Range(0, 100);

        myPointKeeper = GameObject.FindObjectOfType<pointKeeper>();

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
