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
    public GameObject ItemSpawner;
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
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(80,100)});

        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(100,110)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(110,120)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(120,130)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(130,140)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(140,150)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(150,160)});

        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(150,165)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(150,165)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(160,165)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(160,165)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(170,175)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(170,175)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(180,185)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(180,185)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(190,195)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(190,195)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(200,205)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.heart, rowIndex = Random.Range(200,205)});


        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(11,20)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(20,40)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(40,60)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(60,80)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(80,100)});
        
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(100,110)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(110,120)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(120,130)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(130,140)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(140,150)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(150,160)});

        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(150,165)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(160,165)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(170,175)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(180,185)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(190,195)});
        ItemOccurences.Add(new ItemOccurence { iType = eItemType.clear, rowIndex = Random.Range(200,205)});
    }

    public void SpawnItemsForRow(int rowNumber){
        List<ItemOccurence> itemsToSpawn = ItemOccurences.FindAll(delegate(ItemOccurence io){return io.rowIndex <= rowNumber;});

        if(itemsToSpawn.Count > 0){
            foreach (ItemOccurence item in itemsToSpawn){
                //GameObject newItem = Instantiate(PrefabbedItems[(int) item.iType], new Vector3(Random.Range(-3.5f, 3.5f), -3.5f, 0), Quaternion.identity);
                GameObject newItemSpawner = Instantiate(this.ItemSpawner, new Vector3(Random.Range(-3.5f, 3.5f), -2.5f, 0), Quaternion.identity);
                newItemSpawner.GetComponent<itemSpawner>().itemType = item.iType;

                ItemOccurences.Remove(item);
            }
        }
    }

    //Quick debugging feature to spawn items whenever I want
    private void SpawnItem(eItemType itype){
        GameObject newItemSpawner = Instantiate(this.ItemSpawner, new Vector3(Random.Range(-3.5f, 3.5f), -2.5f, 0), Quaternion.identity);
        newItemSpawner.GetComponent<itemSpawner>().itemType = itype;
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
