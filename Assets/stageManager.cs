using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stageManager : MonoBehaviour
{

    Stage CurrentStage;
    Stage NextStage;
    public int CurrentRow = 0;
    public List<Stage> Stages;
    Text stageText;

    void Start(){
        
        this.stageText = transform.Find("CenterText").GetComponent<Text>();

        this.Stages = new List<Stage>();
        this.Stages.Add(new Stage{StartingRow = 0, BgColor = new Color(0.1f, 0.51f, 0.7f)});
        this.Stages.Add(new Stage{StartingRow = 5, BgColor = new Color(0.63f, 0.52f, 0.7f)});
        this.Stages.Add(new Stage{StartingRow = 10, BgColor = new Color(0.8f, 0.04f, 0.7f)});
        this.Stages.Add(new Stage{StartingRow = 15, BgColor = new Color(0.0f, 0.52f, 0.0f)});

        this.NextStage = this.Stages[0];

        CheckForStageIncrease(0);
    }

    public void CheckForStageIncrease(int rowNumber){
        if (NextStage.StartingRow == rowNumber){
            this.HandleStageChange(Stages.IndexOf(NextStage));
        }
    }

    private void Update(){
        //Fade Stage # text away over time.
        if (stageText.color.a > 0){
            stageText.color = new Color(stageText.color.r, stageText.color.g, stageText.color.b, stageText.color.a - (Time.deltaTime * 0.75f)); 
        }
    }

    private void HandleStageChange(int stageNumber)
    {

        stageText.text = "Stage " + (stageNumber + 1);
        stageText.color = new Color(stageText.color.r, stageText.color.g, stageText.color.b, 1);
        stageText.rectTransform.localScale = new Vector3(1,1,1);
        Camera.main.backgroundColor = this.Stages[stageNumber].BgColor;
        if (this.Stages.Count > stageNumber + 1){
            this.NextStage = this.Stages[stageNumber + 1];
        }
    }

}
