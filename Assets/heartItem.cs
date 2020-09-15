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
        if (!this.isMoving) return;

        playerScript playerScr = col.gameObject.GetComponent<playerScript>();
        playerScr.ExtraLives += 1;
        this.isMoving = false;

        Animator animator = GetComponent<Animator>();
        animator.Play("heartCollected");
        Invoke("DestroyMe", 0.3f);

    }

    private void DestroyMe(){
        Destroy(gameObject);
    }
        
}
