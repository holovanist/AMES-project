using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Loot :ScriptableObject
{
    public GameObject LootObject;
    public string LootName;
    public int DropChance;

    public Loot(string lootName, int dropChance)
    {
       this.LootName = lootName;
        this.DropChance = dropChance;
    }
}
