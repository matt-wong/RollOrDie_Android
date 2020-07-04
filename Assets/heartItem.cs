using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartItem : itemMovement
//Item for increase mass of player dice. This should make the rolling take less time
{
    public override bool Equals(object other)
    {
        return base.Equals(other);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    protected override void TouchedPlayer(Collider2D col)
    {
        playerScript playerScr = col.gameObject.GetComponent<playerScript>();
        Destroy(gameObject);
    }

}
