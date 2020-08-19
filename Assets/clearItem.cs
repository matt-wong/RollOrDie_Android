using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Destroy all enemies and obstacles on screen. +2 points.
public class clearItem : itemMovement
{
    public ParticleSystem DeathParticles;
    private enemyManager myEnemyManager;
    private bool needsToResetTime = false; 

    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    void Start() {
        myEnemyManager = GameObject.FindObjectOfType<enemyManager>();
        this.myAnimator = GetComponent<Animator>();
        myStartingTime = Time.time;
    }

    protected override void TouchedPlayer(Collider2D col)
    {

        gameManager.Instance.IncreasePoints(2);
 
        if (myEnemyManager.waitForClearReset){
            if (!this.needsToResetTime){
                Destroy(gameObject);
            }
            return;
        }

        enemyScript[] enemies = GameObject.FindObjectsOfType<enemyScript>();
        foreach (enemyScript es in enemies)
        {
            Vector3 anglePos = es.transform.position - transform.position;
            float angle = Mathf.Atan2(anglePos.y, anglePos.x) * Mathf.Rad2Deg;
            es.GetKilled(angle -90);
        }            

        obstacleScript[] obstacles = GameObject.FindObjectsOfType<obstacleScript>();
        foreach (obstacleScript os in obstacles)
        {
            Vector3 anglePos = os.transform.position - transform.position;
            float angle = Mathf.Atan2(anglePos.y, anglePos.x) * Mathf.Rad2Deg;
            os.GetKilled(angle - 90);
        }

        clearItem[] clearItems = GameObject.FindObjectsOfType<clearItem>();
        foreach (clearItem ci in clearItems)
        {
            if (ci != this)
            {
                Vector3 anglePos = ci.transform.position - transform.position;
                float angle = Mathf.Atan2(anglePos.y, anglePos.x) * Mathf.Rad2Deg;
                ci.GetKilled(angle - 90);
            }
        }

        Animator ani = Camera.main.GetComponent<Animator>();
        ani.Play("CameraZoom");
        
        Time.timeScale = 0.2f;
        this.needsToResetTime = true;
        Invoke("ResetTime", 0.05f);

        //Change animation to exploding boom

        myEnemyManager.HandleAfterClear();

    }

    private void GetKilled(float angle = 0f)
    {
        ParticleSystem ps = Instantiate(DeathParticles, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.Euler(0f, 0f, angle + 40));
        ps.Play();
        Destroy(gameObject);
    }

    private void ResetTime(){
        Time.timeScale = 1f;
        Destroy(gameObject);
    }

}
