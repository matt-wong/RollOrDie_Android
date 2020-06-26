using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weightItem : MonoBehaviour
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


    void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.tag == "Player" && !gameManager.Instance.GameOver)
        {
            Debug.Log(col);
            Rigidbody2D playerRB = col.gameObject.GetComponent<Rigidbody2D>();
            playerRB.mass += 0.25f;
            Destroy(gameObject);
        }
    }
}
