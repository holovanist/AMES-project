using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropChance : MonoBehaviour
{
    [Tooltip("Drop chance mus be 1 more than max")]
    public int maxDropChance = 101;
    public List<Loot> lootList = new List<Loot>();
    public bool DropAllInList;
    public bool DropSpecificAmount;
    public int DropAttemptAmount = 1;

    private void Update()
    {
        if (DropAllInList) DropSpecificAmount = false;
        if (DropSpecificAmount) DropAllInList = false;
    }

    Loot GetDroppedItem()
    {
        int randomNumber = Random.Range(1, maxDropChance);
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.DropChance)
            {
                    possibleItems.Add(item);        
            }
        }
        if (possibleItems.Count > 0)
        {
            Loot droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        Debug.Log("No Loot Dropped");
        return null;
    }

    List<Loot> GetDroppedItemAll()
    {
        int randomNumber = Random.Range(1, 101);
        List<Loot> possibleItems = new List<Loot>();
        foreach (Loot item in lootList)
        {
            if (randomNumber <= item.DropChance)
            {
                possibleItems.Add(item);
            }
        }
        if (possibleItems.Count > 0)
        {
            return possibleItems;
        }
        return null;
    }

    public void InstantiateLoot(Vector3 spawnPosition)
    {
        if (DropSpecificAmount)
        {
            InstantiateLootWithCount(spawnPosition, DropAttemptAmount);
            return;
        }
        if (!DropAllInList)
        {
            Loot droppedItem = GetDroppedItem();
            if (droppedItem != null)
            {
                GameObject lootGameObject = Instantiate(droppedItem.LootObject, spawnPosition, Quaternion.identity);
            }
        }
       
        if (DropAllInList)
        {
            List<Loot> droppedItems = GetDroppedItemAll();
            if (droppedItems != null && droppedItems.Count > 0)
            {
                foreach (Loot droppedItem in droppedItems)
                {
                    GameObject lootGameObject = Instantiate(droppedItem.LootObject, spawnPosition, Quaternion.identity);
                    Debug.Log("loot");
                }
            }
        }
    }
    public void InstantiateLootWithCount(Vector3 spawnPosition, int dropCount)
    {
        List<Loot> droppedItems = new List<Loot>();

        for (int i = 0; i < dropCount; i++)
        {
            Loot droppedItem = GetDroppedItem();
            if (droppedItem != null)
            {
                Instantiate(droppedItem.LootObject, spawnPosition, Quaternion.identity);
                Debug.Log("Loot Dropped: " + droppedItem.LootObject.name);
            }
        }
    }
}