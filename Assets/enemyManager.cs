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
    pointKeeper pointKeeper;

    List<enemyScript> myQueuedEnemies = new List<enemyScript>();
    List<enemyScript> myCurrentEnemies = new List<enemyScript>();
    List<obstacleScript> myQueuedObstacles = new List<obstacleScript>();

    public GameObject myEnemyPrefab;
    public GameObject myObsPrefab;
    public playerScript myPlayerScript;

    private int myObstaclesPerRow = 0;
    private int myBlockedDicePerRow = 0;
    private float myEnemySpeed;

    // Start is called before the first frame update
    void Start()
    {

        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        this.stageManager = canvas.GetComponent<stageManager>();
        if (this.stageManager){
            this.stageManager.NewStageAction += (value) => {this.AdjustToNewStage(value);};
            AdjustToNewStage(this.stageManager.Stages[0]);
            myEnemySpeed = stageManager.GetFirstSpeed();
        }

        this.itemManager = FindObjectOfType<itemManager>();
        this.pointKeeper = FindObjectOfType<pointKeeper>();
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
            if (!es.IsDisabled){
                es.CheckColor(value, pointKeeper.PointMultiplier > 1);
            }
        }

    }

    private void QueueEnemyWave()
    {

        myQueuedEnemies.Clear();

        for (float i = -3.5f; i < 4; i++)
        {
            GameObject newEnemy = Instantiate(myEnemyPrefab, new Vector3(i, 7, 0), Quaternion.identity);
            enemyScript enemyScript1 = newEnemy.GetComponentInChildren<enemyScript>();
            enemyScript1.speed = 0f;
            enemyScript1.CheckColor(myPlayerScript.Value);
            myQueuedEnemies.Add(enemyScript1);
        }

        //Make X number of dice have high values
        int iterations = Math.Min(myBlockedDicePerRow, 7); // Cannot have more than 7 blocked!

        for (int blockIdx = 0; blockIdx < iterations; blockIdx++){
            int diceChoice = UnityEngine.Random.Range(0,8);
            enemyScript choosenToBlock = this.myQueuedEnemies[diceChoice];
            if (choosenToBlock.Value() < 7){
                choosenToBlock.SetAsUnbeatable();
            }else{
                iterations += 1; //This was already selected to be unbeatable try again
            }
        }
    }

    private void AdjustToNewStage(Stage stage){
        Debug.Log("Adjusting to NEW Stage at Row number: " + stage.StartingRow);
        Debug.Log("Speed: " + stage.EnemySpeed);
        Debug.Log("Number Of Obs: " + stage.NumberOfObstacles);
        this.myEnemySpeed = stage.EnemySpeed;
        this.myObstaclesPerRow = stage.NumberOfObstacles;
        this.myBlockedDicePerRow = stage.NumberBlockedDiced;
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

        spawnObstacle(myObstaclesPerRow);

        this.myCurrentEnemies.Clear();
        foreach (enemyScript es in myQueuedEnemies)
        {
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
