using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class enemyScript : MonoBehaviour
{

    const float SPEED = 3.5f;

    int value = 0;
    public bool move = true;
    gameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        TextMesh tm = GetComponentInChildren<TextMesh>();
        this.value = Random.Range(1,7);
        tm.text = this.value.ToString();
        gm = gameManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos= transform.position;
        newPos.z = 0;
        newPos.y -= SPEED * Time.deltaTime;
        transform.position = newPos;
    }
    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.tag == "Player")
        {
            if (!col.gameObject.GetComponent(typeof(playerScript)).Equals(null))
            {
                Debug.Log("PLAYER GET");
                playerScript playerHitScript = (playerScript)col.gameObject.GetComponent(typeof(playerScript));

                if (playerHitScript.value > this.value)
                {
                    //Decrease the players HP so they cannot stay still all day
                    playerHitScript.value -= 1;
                    Debug.Log("dfdas");
                    gm.IncreasePoints(1);
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(col.gameObject);
                    gm.GameOver = true;
                }
            }
        }
        else if(col.tag == "EnemyManager"){
            //End of the page, die now
            Destroy(gameObject);
        }
    }

}
