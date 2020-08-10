using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class enemyScript : fallingObject
{

    
    public DiceFace currFace;
    DiceFace[] faces;

    public Sprite[] faceSprites;
    public ParticleSystem DeathParticles;

    public event System.Action DiedAction;

    private bool myIsDisabled = false;

    private SpriteRenderer mySpriteRenderer;

    void Awake(){
        this.faces = new DiceFace[6];
        for (int i = 0; i < 6; i++)
        {
            this.faces[i] = new DiceFace(i + 1, faceSprites[i]);
        }

        this.currFace = this.faces[Random.Range(0,6)];
        this.mySpriteRenderer = GetComponent<SpriteRenderer>();
        mySpriteRenderer.sprite = this.currFace.sprite;

        Animator animator = GetComponent<Animator>();

        animator.Play("DiceEnemy" + this.currFace.Value.ToString(), -1, Random.Range(0f, 1f));
     }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player" && !myIsDisabled)
        {
            if (!col.gameObject.GetComponent(typeof(playerScript)).Equals(null))
            {
                playerScript playerHitScript = (playerScript)col.gameObject.GetComponent(typeof(playerScript));

                if (playerHitScript.Value > this.currFace.Value && !playerHitScript.IsVulnerable)
                {
                    gm.IncreasePoints(1);
                    GetKilled();
                    //Decrease the players HP so they cannot stay still all day
                    playerHitScript.DecrementValue();
                }


                else if (playerHitScript.ExtraLives > 0)
                {

                    //Player collected a heart make this enemy die now
                    var effects = GameObject.FindObjectOfType<EffectsMaker>();
                    if (effects){
                        effects.HeartEffect(this.transform.position);
                    }

                    playerHitScript.ExtraLives -= 1;
                    gm.IncreasePoints(1);
                    GetKilled();
                }
                else if(playerHitScript.invincible){
                    gm.IncreasePoints(1);
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

    internal void CheckColor(int playerValue)
    {
        if (mySpriteRenderer == null){ return;}

        if (this.currFace.Value >= playerValue){
            this.mySpriteRenderer.color = new Color(1f,0.1f,0.2f); //Light Red
        }else{
            this.mySpriteRenderer.color = new Color(1f,1f,1f);
        }
    }

    public void GetKilled(float angle = 0f)
    {

        Animator ani = Camera.main.GetComponent<Animator>();
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("CameraZoom"))
        {
            ani.Play("CameraShake");
        }

        ParticleSystem ps = Instantiate(DeathParticles, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.Euler(0f, 0f, angle + 40));
        //ps.textureSheetAnimation.SetSprite(0, this.currFace.sprite);
        
        ParticleSystem.MainModule settings = ps.main;
        settings.startColor = new ParticleSystem.MinMaxGradient(this.mySpriteRenderer.color);

        ps.Play();

        if (DiedAction != null){
            DiedAction.Invoke();
        }

        Destroy(gameObject);
    }

    internal void Disable()
    {
        myIsDisabled = true;
    }
}
