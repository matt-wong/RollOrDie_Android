using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyManager : MonoBehaviour
{
    const float SPAWN_RATE = 2.5f; //Seconds

float untilNextSpawn = SPAWN_RATE;
public GameObject myPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        untilNextSpawn -= Time.deltaTime;
        if (untilNextSpawn < 0f){
            untilNextSpawn = SPAWN_RATE;
            this.SpawnEnemyWave();
        }
    }

    private void SpawnEnemyWave()
    {


        for(int i = -5; i < 5; i++){
        Instantiate(myPrefab, new Vector3(i, 8, 0), Quaternion.identity);
        }

    }
}
