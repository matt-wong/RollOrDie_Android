using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawner : MonoBehaviour
{
    public GameObject[] PrefabbedItems;

    public eItemType itemType;
    public float TimeToSpawn = 2f;
    private float timeLeft;
    private float initialXScale; 
    private float initialYScale; 


    // Start is called before the first frame update
    void Start()
    {
     timeLeft = TimeToSpawn;   
     initialXScale = transform.localScale.x;
     initialYScale = transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {

        transform.localScale = new Vector3(timeLeft / TimeToSpawn * initialXScale, timeLeft / TimeToSpawn * initialYScale) ;

        timeLeft -= Time.deltaTime;
        if (timeLeft < 0){
            GameObject newItemSpawner = Instantiate(PrefabbedItems[(int) this.itemType], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
