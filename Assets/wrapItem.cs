using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item for allowing wrapping of player dice. Making it easier to maneuver
public class wrapItem : itemMovement
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
        playerScript playerHitScript = (playerScript)col.gameObject.GetComponent(typeof(playerScript));
        playerHitScript.CanWrap = true;

        AudioSource.PlayClipAtPoint(this.PickUpNoises[UnityEngine.Random.Range(0, this.PickUpNoises.Length)], this.transform.position);
        this.hasBeenCollected = true;
        Destroy(gameObject);
    }

}
