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

    void Start(){
        myEnemyManager = GameObject.FindObjectOfType<enemyManager>();
    }

    protected override void TouchedPlayer(Collider2D col)
    {
        //TODO: Some cool effects!

        Animator ani = Camera.main.GetComponent<Animator>();
        ani.Play("CameraShake");

        enemyScript[] enemies = GameObject.FindObjectsOfType<enemyScript>();
        foreach (enemyScript es in enemies)
        {
            Destroy(es.gameObject);
        }

        obstacleScript[] obstacles = GameObject.FindObjectsOfType<obstacleScript>();
        foreach (obstacleScript os in obstacles)
        {
            Destroy(os.gameObject);
        }

        gameManager.Instance.Points += 2;
        myEnemyManager.RowsSpawned += 2;
        myEnemyManager.HandleAfterClear();
        Destroy(gameObject);
    }

}
