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
    itemManager itemManager;

    const int OBSTACLE_START_ROW_1 = 20;
    const int OBSTACLE_START_ROW_2 = 30;
    const int OBSTACLE_START_ROW_3 = 40;

    List<enemyScript> myQueuedEnemies = new List<enemyScript>();
    List<obstacleScript> myQueuedObstacles = new List<obstacleScript>();

    public GameObject myEnemyPrefab;
    public GameObject myObsPrefab;
    // Start is called before the first frame update
    void Start()
    {
        untilNextSpawn = spawnRate;
        this.QueueEnemyWave();
        this.SendWave();
        this.QueueEnemyWave();

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.stageManager = canvas.GetComponent<stageManager>();
        this.itemManager = FindObjectOfType<itemManager>();
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
            GameObject newEnemy = Instantiate(myEnemyPrefab, new Vector3(i, 7, 0), Quaternion.identity);
            enemyScript enemyScript1 = newEnemy.GetComponentInChildren<enemyScript>();
            enemyScript1.speed = 0f;
            myQueuedEnemies.Add(enemyScript1);
        }

        if (rowsSpawned >= OBSTACLE_START_ROW_3 - 1)
        {
            spawnObstacle(3);
        }
        else if (rowsSpawned >= OBSTACLE_START_ROW_2 - 1)
        {
            spawnObstacle(2);
        }
        else if (rowsSpawned >= OBSTACLE_START_ROW_1 - 1)
        {
            spawnObstacle(1);
        }

    }

    private void spawnObstacle(int numOfObstacles){

        for (int i = 0; i < numOfObstacles; i++){
            float obsX = UnityEngine.Random.Range(-4, 4) + 0.5f;

            GameObject newObstacle = Instantiate(myObsPrefab, new Vector3(obsX, UnityEngine.Random.Range(8f, 19f), 0), Quaternion.identity);
            obstacleScript obsScript = newObstacle.GetComponentInChildren<obstacleScript>();
            myQueuedObstacles.Add(obsScript);
        }
    }

    private void SendWave(){
        foreach (enemyScript es in myQueuedEnemies){
            es.speed = SpeedFromRowIndex(this.rowsSpawned);
        }

        foreach (obstacleScript os in myQueuedObstacles){
            os.speed = SpeedFromRowIndex(this.rowsSpawned);
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
                itemManager.SpawnItemsForRow(rowsSpawned);
            }
            lastSpawnTime = Time.fixedTime;
        }

    }

    private float SpeedFromRowIndex(int rowsSpawned)
    {
        //Linear relation until keep speed at row 20
        return  0.25f * Math.Min(rowsSpawned, 20) + 3.5f;
    }
}
