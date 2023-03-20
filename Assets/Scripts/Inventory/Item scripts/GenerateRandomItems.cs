using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomItems : MonoBehaviour
{
    public InventoryItemData[] lootItemsList = new InventoryItemData[] { };
    public ChestInventory chest;
    public LootManager pattern;

    void Start()
    {
        chest = this.gameObject.GetComponent<ChestInventory>();
        lootItemsList = Resources.LoadAll<InventoryItemData>("ScriptableObjects");

        /* if(pattern != null) GenerateItems(pattern);
         else GenerateItems();*/
        GenerateItems();
    }

    private void Awake()
    {
        
    }

    private void GenerateItems()
    {
        int randomArrayPos;
        double randomNumber = Random.Range(0, (float)CalculateTotalWeight(lootItemsList));

        for(int i = 0; i < Random.RandomRange(0, 15); i++)
        {
            foreach (var item in lootItemsList)
            {
                if (randomNumber < item.weight)
                {
                    chest.InventorySystem.AddToInventory(item, 1);
                    break;
                }

                randomNumber = randomNumber - item.weight;
            }
        }
    }

    private void GenerateItems(LootManager pattern)
    {
        int randomArrayPos;
        double randomNumber = Random.Range(0, (float)CalculateTotalWeight(lootItemsList));

        for (int i = 0; i < Random.RandomRange(0, 15); i++)
        {
            if(pattern.itemType.Equals("All"))
            {
                foreach (var item in lootItemsList)
                {
                    if (randomNumber < item.weight)
                    {
                        chest.InventorySystem.AddToInventory(item, 1);
                        break;
                    }

                    randomNumber -= item.weight;
                }
            }
            else
            {
                foreach (var item in lootItemsList)
                {
                    if (item.type.Equals(pattern.itemType))
                    {
                        if (randomNumber < item.weight)
                        {
                            chest.InventorySystem.AddToInventory(item, 1);
                            break;
                        }
                    }

                    randomNumber -= item.weight;
                }
            }
        }
    }

    private double CalculateTotalWeight(InventoryItemData[] lootItemsList)
    {
        double totalWeigh = 0;

        foreach (var item in lootItemsList)
        {
            totalWeigh += item.weight;
        }

        return totalWeigh;
    }
}
