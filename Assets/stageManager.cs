using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class stageManager : MonoBehaviour
{
    public event System.Action<Stage> NewStageAction; 
    public event System.Action GameWon; 
    public const int LAST_SECTION_START_INDEX = 160;

    private AudioSource myAudSource;
    public AudioClip myWinNoise;

    Stage CurrentStage;
    Stage NextStage;
    public int CurrentRow = 0;
    public List<Stage> Stages;
    private List<ParticleSystem> bgParticleSystems;
    private SpriteRenderer bgFader;
    TextMeshProUGUI stageText;

    public Sprite[] BlobSprites;
    private int bgSpriteIdx = 0;

    private Button leftButton;
    private Button rightButton;
    private Button rollButton;
    private Image dPadBacking;
    private Image scoreTextPanel;
    private Image itemInventoryPanel;

    private const float BG_ALPHA = 0.8f;
    private Color BG_BLUE = new Color(0.568f, 0.686f, 0.792f);
    private Color BG_PURPLE = new Color(0.874f, 0.807f, 0.890f);
    private Color BG_RED = new Color(0.901f, 0.776f, 0.756f);
    private Color BG_ORANGE = new Color(0.921f, 0.831f, 0.756f);
    private Color BG_YELLOW = new Color(0.909f, 0.921f, 0.756f);
    private Color BG_GREEN = new Color(0.756f, 0.921f, 0.8f);

    void Awake(){
        this.Stages = new List<Stage>();
        this.Stages.Add(new Stage { StartingRow = 0, BgColor = BG_BLUE, NumberOfObstacles = 0, NumberBlockedDiced = 0, MusicSpeed = 1}); //Normal
        this.Stages.Add(new Stage { StartingRow = 5, BgColor = BG_PURPLE, NumberOfObstacles = 0, Description = "SPEED UP!", MusicSpeed = 1.0125f}); //Faster
        // this.Stages.Add(new Stage { StartingRow = 10, BgColor = BG_RED, NumberOfObstacles = 0, Description = "SPEED UP!", MusicSpeed = 1.025f}); //Item / Faster - Red
        // this.Stages.Add(new Stage { StartingRow = 15, BgColor = BG_ORANGE, NumberOfObstacles = 0, Description = "SPEED UP!", MusicSpeed = 1.0375f}); //Faster - Orange
        // this.Stages.Add(new Stage { StartingRow = 20, BgColor = BG_YELLOW, NumberOfObstacles = 1, Description = "AVOID SPIKEYS!", MusicSpeed = 1.05f}); //1 Obstacle / row - Yellow 
        // this.Stages.Add(new Stage { StartingRow = 30, BgColor = BG_GREEN, NumberOfObstacles = 2, Description = "AVOID MORE SPIKEYS!"}); //2 Obstacle / row -Green
        
        // this.Stages.Add(new Stage { StartingRow = 40, BgColor = BG_BLUE, NumberOfObstacles = 3, Description = "MORE SPIKEY GUYS!"}); //3 Obstacle / row -New Blob Sprite
        // this.Stages.Add(new Stage { StartingRow = 50, BgColor = BG_PURPLE, NumberOfObstacles = 4, Description = "CONTINUE AVOIDING!"}); //4 Obstacles
        // this.Stages.Add(new Stage { StartingRow = 60, BgColor = BG_RED, NumberOfObstacles = 4, NumberBlockedDiced = 1, Description = "BLOCKED DICE!"}); // 5 Obstacles
        // this.Stages.Add(new Stage { StartingRow = 70, BgColor = BG_ORANGE, NumberOfObstacles = 5, NumberBlockedDiced = 1, Description = "MORE SPIKEY GUYS!"}); // 6 Obstacles
        // this.Stages.Add(new Stage { StartingRow = 80, BgColor = BG_YELLOW, NumberOfObstacles = 6, NumberBlockedDiced = 1, Description = "+1 SPIKEY GUY!"}); // 7 Obstacles
        // this.Stages.Add(new Stage { StartingRow = 90, BgColor = BG_GREEN, NumberOfObstacles = 6, NumberBlockedDiced = 2, Description = "+1 BLOCKED DICE!"}); // 8 Obs
        
        // this.Stages.Add(new Stage { StartingRow = 100, BgColor = BG_BLUE, NumberOfObstacles = 7, NumberBlockedDiced = 2, Description = "MORE SPIKEY GUYS!"}); 
        // this.Stages.Add(new Stage { StartingRow = 110, BgColor = BG_PURPLE, NumberOfObstacles = 8, NumberBlockedDiced = 2, Description = "MORE!"}); 
        // this.Stages.Add(new Stage { StartingRow = 120, BgColor = BG_RED, NumberOfObstacles = 8, NumberBlockedDiced = 3, Description = "YET ANOTHER BLOCKED DICE!"}); 
        // this.Stages.Add(new Stage { StartingRow = 130, BgColor = BG_ORANGE, NumberOfObstacles = 9, NumberBlockedDiced = 3, Description = "Guess what?!"});
        // this.Stages.Add(new Stage { StartingRow = 140, BgColor = BG_YELLOW, NumberOfObstacles = 10, NumberBlockedDiced = 3, Description = "ANOTHER SPIKEY GUY!"});
        // this.Stages.Add(new Stage { StartingRow = 150, BgColor = BG_GREEN, NumberOfObstacles = 10, NumberBlockedDiced = 4, Description = "Please avoid them."});
        
        // this.Stages.Add(new Stage { StartingRow = 160, BgColor = BG_BLUE, NumberOfObstacles = 10, NumberBlockedDiced = 4, Description = "Please..."});
        // this.Stages.Add(new Stage { StartingRow = 170, BgColor = BG_PURPLE, NumberOfObstacles = 11, NumberBlockedDiced = 4, Description = "KEEP IT GOING!"});
        // this.Stages.Add(new Stage { StartingRow = 180, BgColor = BG_RED, NumberOfObstacles = 12, NumberBlockedDiced = 4, Description = "Almost there...!"});
        // this.Stages.Add(new Stage { StartingRow = 190, BgColor = BG_ORANGE, NumberOfObstacles = 13, NumberBlockedDiced = 5, Description = "!!!!AHH!!"});
        // this.Stages.Add(new Stage { StartingRow = 200, BgColor = BG_GREEN, NumberOfObstacles = 14, NumberBlockedDiced = 5, Description = "SUCCESS!! Thanks for playing!"});

        foreach(Stage stg in this.Stages){
            stg.EnemySpeed = SpeedFromRowIndex(stg.StartingRow, gameManager.Instance.difficulty);
            stg.MusicSpeed = 1 + (stg.StartingRow * 0.0025f);
        }

        this.NextStage = this.Stages[0];
    }

    void Start(){

        this.myAudSource  = GetComponent<AudioSource>();

        this.leftButton  = GameObject.Find("leftButton").GetComponent<Button>();
        this.rightButton  = GameObject.Find("rightButton").GetComponent<Button>();
        this.rollButton  = GameObject.Find("rollButton").GetComponent<Button>();
        // this.dPadBacking  = GameObject.Find("dPadBacking").GetComponent<Image>();
        // this.scoreTextPanel = GameObject.Find("scoreTextPanel").GetComponent<Image>();
        // this.itemInventoryPanel = GameObject.Find("ItemInventoryDisplay").GetComponent<Image>();

        this.stageText = transform.Find("CenterText").GetComponent<TextMeshProUGUI>();
        this.bgFader = GameObject.Find("BackgroundFader").GetComponent<SpriteRenderer>();

        this.bgFader.color = new Color(BG_BLUE.r, BG_BLUE.g, BG_BLUE.b, BG_ALPHA);
        
        bgParticleSystems = new List<ParticleSystem>();
        ParticleSystem[] partSyses = GameObject.Find("BackgroundParticles").GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem ps in partSyses)
        {
                bgParticleSystems.Add(ps);
        }

        CheckForStageIncrease(0);
    }

    static float SpeedFromRowIndex(int rowsSpawned, eDifficulty diff)
    {
        if (diff == eDifficulty.spicy)
        {
            //Linear relation until keep speed at row 20
            return 0.25f * Math.Min(rowsSpawned, 20) + 3.5f;
        }
        else //(gameManager.Instance.difficulty == eDifficulty.easy)
        {
            //Linear relation until keep speed at row 20
            return 0.15f * Math.Min(rowsSpawned, 20) + 3.5f;
        }
    }

    public float GetFirstSpeed()
    {
        return SpeedFromRowIndex(0, gameManager.Instance.difficulty);
    }

    public void CheckForStageIncrease(int rowNumber){
        if (NextStage == null) return; //No more stages past this point

        if (NextStage.StartingRow <= rowNumber){
            this.HandleStageChange(Stages.IndexOf(NextStage));
        }

    }

    private void Update(){
        //Fade Stage # text away over time.
        if (stageText.color.a > 0 ){
            float fadeRate = 0.5f;
            if (gameManager.Instance.GameWasWon){
                fadeRate = 0.2f;
            }
            stageText.color = new Color(stageText.color.r, stageText.color.g, stageText.color.b, stageText.color.a - (Time.deltaTime * fadeRate)); 
        }

        if(gameManager.Instance.IsPaused){
            Color softWhite = new Color(1f, 1f, 1f, 0.8f);
            leftButton.image.color = softWhite;
            rightButton.image.color = softWhite;
            rollButton.image.color = softWhite;
            this.bgFader.color = softWhite;

        }else if (this.bgFader.color != this.CurrentStage.BgColor){
            Color gbTransparent = new Color(this.CurrentStage.BgColor.r, this.CurrentStage.BgColor.g, this.CurrentStage.BgColor.b, 0.8f);
            this.bgFader.color = Color.Lerp( this.bgFader.color, gbTransparent, Time.deltaTime);
            
            Color uiLerpColor = Color.Lerp(leftButton.image.color, this.CurrentStage.BgColor, Time.deltaTime);
            leftButton.image.color = uiLerpColor;
            rightButton.image.color = uiLerpColor;
            rollButton.image.color = uiLerpColor;
        }
    }

    private void HandleStageChange(int stageNumber)
    {

        if (this.myAudSource && stageNumber != 0 && stageNumber + 1 < this.Stages.Count){
            this.myAudSource.Play();
        }

        this.CurrentStage = this.Stages[stageNumber];

        stageText.text = "Stage " + (stageNumber + 1) + "\n " + this.CurrentStage.Description;
        //Opacity full
        stageText.color = new Color(stageText.color.r, stageText.color.g, stageText.color.b, 1);
        stageText.rectTransform.localScale = new Vector3(1,1,1);

        int bgSpriteIdx = stageNumber / 6;
       
        foreach (ParticleSystem ps in this.bgParticleSystems)
        {
            ps.textureSheetAnimation.SetSprite(0, this.BlobSprites[Math.Min(bgSpriteIdx, this.BlobSprites.Length - 1)]);
        }


        if (this.Stages.Count > stageNumber + 1){
            this.NextStage = this.Stages[stageNumber + 1];
        }else{ 

            this.myAudSource.clip = myWinNoise;
            this.myAudSource.Play();

            //This was the last Stage - You WIN!
            Debug.Log("GAME WAS WOOOOONNN!");
            this.NextStage = null;
            gameManager.Instance.GameWasWon = true;
            this.GameWon.Invoke();
        }

        this.NewStageAction.Invoke(this.CurrentStage);
    }

}
