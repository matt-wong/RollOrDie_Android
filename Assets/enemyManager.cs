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

    List<enemyScript> myQueuedEnemies = new List<enemyScript>();

public GameObject myPrefab;
    // Start is called before the first frame update
    void Start()
    {
        untilNextSpawn = spawnRate;
        this.QueueEnemyWave();
        this.SendWave();
        this.QueueEnemyWave();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void QueueEnemyWave()
    {

        myQueuedEnemies.Clear();

        for (int i = -4; i < 4; i++)
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
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            //It's been 0.5 secs since the last enemy touched the despawner; 
            if (Time.fixedTime - lastSpawnTime > 0.5f || lastSpawnTime == 0)
            {
                SendWave();
                this.QueueEnemyWave();
                rowsSpawned += 1;
            }
            lastSpawnTime = Time.fixedTime;
        }

    }

    private float SpeedFromRowIndex(int rowsSpawned)
    {
        return 0.25f * rowsSpawned + 3.5f;
    }
}
