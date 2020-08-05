using System;

[Serializable]
public class saveFile
{
    public int Highscore = 0;
    public int cumulativeScore = 0;
    public int UnlockIndex = 0;

    public saveFile(int hs, int cs, int unlockIndex)
    {
        this.Highscore = hs;
        this.cumulativeScore = cs;
        this.UnlockIndex = unlockIndex;
    }
    

}