using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawner : MonoBehaviour
{
    public GameObject[] PrefabbedItems;

    public eItemType itemType;
    public float TimeToSpawn = 2f;
    private float initialXScale; 
    private float initialYScale; 


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeToSpawn -= Time.deltaTime;
        if (TimeToSpawn < 0){
            GameObject newItemSpawner = Instantiate(PrefabbedItems[(int) this.itemType], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
