using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weightItem : itemMovement
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
        Rigidbody2D playerRB = col.gameObject.GetComponent<Rigidbody2D>();
        playerScript playerHitScript = (playerScript)col.gameObject.GetComponent(typeof(playerScript));
        playerHitScript.HasExtraWeight = true;
        playerRB.mass += 0.3f;
        Destroy(gameObject);
    }

}
