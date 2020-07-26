using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScript : fallingObject
{

    public DiceFace currFace;
    DiceFace[] faces;

    public Sprite[] faceSprites;
    public ParticleSystem DeathParticles;

    void Awake(){
        this.faces = new DiceFace[6];
        for (int i = 0; i < 6; i++)
        {
            this.faces[i] = new DiceFace(i + 1, faceSprites[i]);
        }

        this.currFace = this.faces[Random.Range(0,6)];
        SpriteRenderer spr = GetComponent<SpriteRenderer>();
        spr.sprite = this.currFace.sprite;
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
                    GetKilled();
                    //Decrease the players HP so they cannot stay still all day
                    playerHitScript.DecrementValue();
                }


                else if (playerHitScript.ExtraLives > 0)
                {
                    //Player collected a heart make this enemy die now
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

    public void GetKilled(float angle = 0f)
    {
        Animator ani = Camera.main.GetComponent<Animator>();
        ani.Play("CameraShake");
        ParticleSystem ps = Instantiate(DeathParticles, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.Euler(0f, 0f, angle + 40));
        ps.textureSheetAnimation.SetSprite(0, this.currFace.sprite);
        ps.Play();

        Debug.Log(ps.shape.shapeType);

        gm.IncreasePoints(1);
        Destroy(gameObject);
    }

}
