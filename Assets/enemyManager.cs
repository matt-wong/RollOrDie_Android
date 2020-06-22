using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyManager : MonoBehaviour
{
    float spawnRate = 2.5f; //Seconds
    int rowsSpawned = 0;
    float untilNextSpawn;
    float lastSpawnTime = 0;
    stageManager stageManager;


    List<enemyScript> myQueuedEnemies = new List<enemyScript>();

public GameObject myPrefab;
    // Start is called before the first frame update
    void Start()
    {
        untilNextSpawn = spawnRate;
        this.QueueEnemyWave();
        this.SendWave();
        this.QueueEnemyWave();

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.stageManager = canvas.GetComponent<stageManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void QueueEnemyWave()
    {

        myQueuedEnemies.Clear();

        for (float i = -3.5f; i < 4; i++)
        {
            GameObject newEnemy = Instantiate(myPrefab, new Vector3(i, 7, 0), Quaternion.identity);
            enemyScript enemyScript1 = newEnemy.GetComponentInChildren<enemyScript>();
            enemyScript1.speed = 0f;
            myQueuedEnemies.Add(enemyScript1);
        }

    }

    private void SendWave(){
        foreach (enemyScript es in myQueuedEnemies){
            es.speed = SpeedFromRowIndex(this.rowsSpawned);
        }

        //Keep track of the easiest enemy to beat so we can rig the players dice rolls to win sshhhhh...
        gameManager.Instance.weakestEnemyHP = Int16.MaxValue;
        foreach (enemyScript es in myQueuedEnemies)
        {
            gameManager.Instance.weakestEnemyHP = System.Math.Min(gameManager.Instance.weakestEnemyHP, es.currFace.Value);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy" && !gameManager.Instance.GameOver)
        {
            //It's been 0.25 secs since the last enemy touched the despawner.
            // We check this so that multiple enemies in the same row don't trigger the new row.
            if (Time.fixedTime - lastSpawnTime > 0.25f || lastSpawnTime == 0)
            {
                SendWave();
                this.QueueEnemyWave();
                rowsSpawned += 1;
                stageManager.CheckForStageIncrease(rowsSpawned);
            }
            lastSpawnTime = Time.fixedTime;
        }

    }

    private float SpeedFromRowIndex(int rowsSpawned)
    {
        return 0.25f * rowsSpawned + 3.5f;
    }
}
