using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageManager : MonoBehaviour
{

    int myStage = 0; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.Instance.StageNumber != myStage)
        {
            HandleStageChange(gameManager.Instance.StageNumber);
            myStage = gameManager.Instance.StageNumber;
        }
    }

        public void HandleStageChange(int stageNumber)
    {
        Debug.Log("Stage " + stageNumber);

        Camera.main.backgroundColor = new Color(162f/256f, 137f/256f, 179f/256f);
    }

}
