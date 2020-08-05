using UnityEngine;

public class unlockable{

    public Sprite reward; //Could be anything, but lets do a sprite for better visualization
    public int scoreGoal; 
    public int cumulativeScoreGoal; 

    public unlockable(int sg, int csg, Sprite sprite){
        this.scoreGoal = sg;
        this.cumulativeScoreGoal = csg;
        this.reward = sprite;
    }

}