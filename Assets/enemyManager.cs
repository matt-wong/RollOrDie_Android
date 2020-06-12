using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyManager : MonoBehaviour
{
    float spawnRate = 2.5f; //Seconds
    int rowsSpawned = 0;
    float untilNextSpawn;

public GameObject myPrefab;
    // Start is called before the first frame update
    void Start()
    {
 untilNextSpawn = spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        untilNextSpawn -= Time.deltaTime;
        if (untilNextSpawn < 0f){
            spawnRate -= 0.1f;
            untilNextSpawn = spawnRate;
            this.SpawnEnemyWave();
            rowsSpawned += 1;
        }
    }

    private void SpawnEnemyWave()
    {


        for(int i = -5; i < 5; i++){
        GameObject newEnemy = Instantiate(myPrefab, new Vector3(i, 8, 0), Quaternion.identity);
            enemyScript enemyScript1 = newEnemy.GetComponentInChildren<enemyScript>(); 
            enemyScript1.speed = SpeedFromRowIndex(this.rowsSpawned);
        }

    }

    private float SpeedFromRowIndex(int rowsSpawned)
    {
        return 0.5f * rowsSpawned + 2.5f;
    }
}
