using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Destroy all enemies and obstacles on screen. +2 points.
public class clearItem : itemMovement
{

    private enemyManager myEnemyManager;

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

        Animator ani = Camera.main.GetComponent<Animator>();
        ani.Play("CameraShake");
        gameManager.Instance.IncreasePoints(2);
 
        if (myEnemyManager.waitForClearReset){
            Destroy(gameObject);
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
            Destroy(os.gameObject);
        }

        myEnemyManager.HandleAfterClear();
        Destroy(gameObject);

    }

}
