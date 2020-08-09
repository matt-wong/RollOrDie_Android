using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnlockData{
    public static int UnlockIndex = 0;
    public static int SumScore = 0;

    public static string Status;
}

public class unlockableManager : MonoBehaviour
{

    public Sprite[] gRewards;
    private List<unlockable> Unlockables;
    public string UnlockStatus;

    public void MaybeUnlockNext(int roundScore)
    {
        unlockable unlockMe = Unlockables[UnlockData.UnlockIndex];

        if(unlockMe.cumulativeScoreGoal <= UnlockData.SumScore || unlockMe.scoreGoal <= roundScore){
            Debug.Log("UNLOCKED " + UnlockData.UnlockIndex);
            UnlockData.Status = "NEW UNLOCK" + UnlockData.UnlockIndex;
            UnlockData.UnlockIndex += 1;
        }else{
            Debug.Log("Not unlocked!");
            UnlockData.Status = "Next Unlock: " + UnlockData.SumScore + " / " + unlockMe.cumulativeScoreGoal + " OR highscore: " + unlockMe.scoreGoal;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        Unlockables = new List<unlockable>();
        Unlockables.Add(new unlockable(10, 20, gRewards[0]));
        Unlockables.Add(new unlockable(20, 40, gRewards[1]));
        Unlockables.Add(new unlockable(30, 90, gRewards[2]));
    }

}
