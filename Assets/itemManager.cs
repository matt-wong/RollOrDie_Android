using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eItemType{
    weight = 0,
    heart = 1,
    wrap = 2,
    clear = 3
}

public class itemManager : MonoBehaviour
{

    public GameObject[] PrefabbedItems;
    private List<ItemOccurence> ItemOccurences;

    public void Start()
    {
        ItemOccurences = new List<ItemOccurence>();

        ItemOccurences.Add(new ItemOccurence { iType = eItemType.weight, rowIndex = 10});

        ItemOccurences.Add(new ItemOccurence { iType = eItemType.wrap, rowIndex = 30});
        
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(11,20)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(20,40)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(40,60)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(60,80)});

        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(11,20)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(20,40)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(40,60)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(60,80)});
    }

    public void SpawnItemsForRow(int rowNumber){
        List<ItemOccurence> itemsToSpawn = ItemOccurences.FindAll(delegate(ItemOccurence io){return io.rowIndex == rowNumber;});

        if(itemsToSpawn.Count > 0){
            foreach (ItemOccurence item in itemsToSpawn){
                GameObject newItem = Instantiate(PrefabbedItems[(int) item.iType], new Vector3(Random.Range(-3.5f, 3.5f), -3.5f, 0), Quaternion.identity);
            }
        }
    }

    //Quick debugging feature to spawn items whenever I want
    public void SpawnItem(eItemType itype){
        GameObject newItem = Instantiate(PrefabbedItems[(int) itype], new Vector3(Random.Range(-3.5f, 3.5f), -3.5f, 0), Quaternion.identity);
    }

    void Update(){
        if (Input.GetKeyDown(KeyCode.Q))
        {
            this.SpawnItem(eItemType.clear);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            this.SpawnItem(eItemType.heart);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            this.SpawnItem(eItemType.weight);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            this.SpawnItem(eItemType.wrap);
        }
    }
}
