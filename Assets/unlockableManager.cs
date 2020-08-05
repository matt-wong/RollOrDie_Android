using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unlockableManager : MonoBehaviour
{

    public Sprite[] gRewards;
    private List<unlockable> Unlockables;
    public int UnlockIndex = 0;
    public int SumScore = 0;

    public void MaybeUnlockNext(int roundScore)
    {
        unlockable unlockMe = Unlockables[UnlockIndex];

        if(unlockMe.cumulativeScoreGoal <= SumScore || unlockMe.scoreGoal <= roundScore){
            Debug.Log("UNLOCKED " + UnlockIndex);
            UnlockIndex += 1;
        }else{
            Debug.Log("Not unlocked!");
            Debug.Log("unlockMe::ScoreGoal" + unlockMe.scoreGoal);
            Debug.Log("unlockMe::SumGoal" + unlockMe.cumulativeScoreGoal);
            Debug.Log("Points " + roundScore);
            Debug.Log("Sum Points " + SumScore);
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
