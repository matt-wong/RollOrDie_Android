using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageManager : MonoBehaviour
{

    Stage CurrentStage;
    Stage NextStage;
    public int CurrentRow = 0;
    public List<Stage> Stages;

    void Start(){
        this.Stages = new List<Stage>();
        this.Stages.Add(new Stage{StartingRow = 3, BgColor = new Color(0.63f, 0.52f, 0.7f)});
        this.Stages.Add(new Stage{StartingRow = 5, BgColor = new Color(0.8f, 0.04f, 0.7f)});
        this.Stages.Add(new Stage{StartingRow = 7, BgColor = new Color(0.0f, 0.52f, 0.0f)});

        this.NextStage = this.Stages[0];
    }

    public void CheckForStageIncrease(int rowNumber){
        if (NextStage.StartingRow == rowNumber){
            this.HandleStageChange(Stages.IndexOf(NextStage));
        }
    }

    private void HandleStageChange(int stageNumber)
    {
        Debug.Log("Stage " + stageNumber);
        Camera.main.backgroundColor = this.Stages[stageNumber].BgColor;
        if (this.Stages.Count > stageNumber + 1){
            this.NextStage = this.Stages[stageNumber + 1];
        }
    }

}
