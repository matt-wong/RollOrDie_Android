using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class heartItem : itemMovement
//Item for increase lives of player dice.
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
        playerScr.ExtraLives += 1;
        this.hasBeenCollected = true;
        AudioSource.PlayClipAtPoint(this.PickUpNoises[UnityEngine.Random.Range(0, this.PickUpNoises.Length)], this.transform.position);

        Animator animator = GetComponent<Animator>();
        animator.Play("heartCollected");
        Invoke("DestroyMe", 0.3f);

    }

    private void DestroyMe(){
        Destroy(gameObject);
    }
        
}
