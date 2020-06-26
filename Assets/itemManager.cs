using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType{
    weight = 0,
    wrap = 1,
    clear = 2,
    live = 3,
    beam = 4
}

public class itemManager : MonoBehaviour
{

    public GameObject[] PrefabbedItems;
    private List<ItemOccurence> ItemOccurences;

    public void Start()
    {
        ItemOccurences = new List<ItemOccurence>();

        ItemOccurences.Add(new ItemOccurence { iType = itemType.weight, rowIndex = 10, fadeTime = -1f });
    }

    public void SpawnItemsForRow(int rowNumber){
        List<ItemOccurence> itemsToSpawn = ItemOccurences.FindAll(delegate(ItemOccurence io){return io.rowIndex == rowNumber;});

        if(itemsToSpawn.Count > 0){
            foreach (ItemOccurence item in itemsToSpawn){
                GameObject newItem = Instantiate(PrefabbedItems[(int) item.iType], new Vector3(Random.Range(-3.5f, 3.5f), -4, 0), Quaternion.identity);
            }
        }
    }
}
