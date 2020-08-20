using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class enemyManager : MonoBehaviour
{

    float lastSpawnTime = 0;
    public bool waitForClearReset = false;
    stageManager stageManager;
    itemManager itemManager;

    const int OBSTACLE_START_ROW_1 = 20;
    const int OBSTACLE_START_ROW_2 = 30;
    const int OBSTACLE_START_ROW_3 = 40;
    const int OBSTACLE_START_ROW_4 = 50;
    const int OBSTACLE_START_ROW_5 = 60;
    const int OBSTACLE_START_ROW_6 = 70;

    List<enemyScript> myQueuedEnemies = new List<enemyScript>();
    List<enemyScript> myCurrentEnemies = new List<enemyScript>();
    List<obstacleScript> myQueuedObstacles = new List<obstacleScript>();

    public GameObject myEnemyPrefab;
    public GameObject myObsPrefab;
    public playerScript myPlayerScript;

    private int myObstaclesPerRow = 0;
    private float myEnemySpeed;

    // Start is called before the first frame update
    void Start()
    {

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.stageManager = canvas.GetComponent<stageManager>();
        if (this.stageManager){
            this.stageManager.NewStageAction += (value) => {this.AdjustToNewStage(value);};
            myEnemySpeed = stageManager.GetFirstSpeed();
        }

        this.itemManager = FindObjectOfType<itemManager>();
        this.myPlayerScript = FindObjectOfType<playerScript>();
        this.myPlayerScript.NewValueAction += (value) => {this.UpdateEnemyColors(value);};

        this.QueueEnemyWave();
        this.SendWave();
        this.QueueEnemyWave();


    }

    private void UpdateEnemyColors(int value){
        foreach (enemyScript es in myQueuedEnemies){
            es.CheckColor(value);
        }

        foreach (enemyScript es in myCurrentEnemies){
            es.CheckColor(value);
        }

    }

    private void QueueEnemyWave()
    {

        myQueuedEnemies.Clear();

        for (float i = -3.5f; i < 4; i++)
        {
            GameObject newEnemy = Instantiate(myEnemyPrefab, new Vector3(i, 7, 0), Quaternion.identity);
            enemyScript enemyScript1 = newEnemy.GetComponentInChildren<enemyScript>();
            enemyScript1.CheckColor(myPlayerScript.Value);
            enemyScript1.speed = 0f;
            myQueuedEnemies.Add(enemyScript1);
        }

        spawnObstacle(myObstaclesPerRow);

    }

    private void AdjustToNewStage(Stage stage){
        Debug.Log("Adjusting to NEW Stage at Row number: " + stage.StartingRow);
        Debug.Log("Speed: " + stage.EnemySpeed);
        Debug.Log("Number Of Obs: " + stage.numberOfObstacles);
        this.myEnemySpeed = stage.EnemySpeed;
        this.myObstaclesPerRow = stage.numberOfObstacles;
    }

    public void HandleAfterClear(){
        if (this.waitForClearReset == true){
            //A clear reset is happening, don't bother re-spawning stuff.
            return;
        }
        this.waitForClearReset = true;
        this.QueueEnemyWave();
        Invoke("SendAndQueue", 2);//this will happen after 2 seconds
    }

    private void SendAndQueue(){
        this.SendWave();
        this.QueueEnemyWave();
        this.waitForClearReset = false;
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
        this.myCurrentEnemies.Clear();
        foreach (enemyScript es in myQueuedEnemies){
            es.speed = myEnemySpeed;
            es.DiedAction += this.disableRow;
            myCurrentEnemies.Add(es);
        }

        foreach (obstacleScript os in myQueuedObstacles){
            os.speed = myEnemySpeed;
        }

        //Keep track of the easiest enemy to beat so we can rig the players dice rolls to win sshhhhh...
        gameManager.Instance.weakestEnemyHP = Int16.MaxValue;
        foreach (enemyScript es in myQueuedEnemies)
        {
            gameManager.Instance.weakestEnemyHP = System.Math.Min(gameManager.Instance.weakestEnemyHP, es.currFace.Value);

        }
    }

    public void disableRow()
    {
        foreach (enemyScript es in myCurrentEnemies){
            es.Disable();
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

                stageManager.CheckForStageIncrease(gameManager.Instance.Points);
                itemManager.SpawnItemsForRow(gameManager.Instance.Points);
            }
            lastSpawnTime = Time.fixedTime;
        }

    }
}
