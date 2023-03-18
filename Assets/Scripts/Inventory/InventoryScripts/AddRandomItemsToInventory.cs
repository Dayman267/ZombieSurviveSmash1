using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRandomItemsToInventory : MonoBehaviour
{
    public InventoryItemData[] lootItemsList = new InventoryItemData[] { };
    public ChestInventory chest ;

    void Start()
    {
        chest = this.gameObject.GetComponent<ChestInventory>();
        lootItemsList = Resources.LoadAll<InventoryItemData>("ScriptableObjects");
        for (int i = 0; i < Random.Range(1, 10); i++)
        {
            chest.InventorySystem.AddToInventory(lootItemsList[Random.Range(0, lootItemsList.Length)],1);
        }
        
    }
}
