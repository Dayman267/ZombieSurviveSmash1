using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Mirror;
using UnityEngine;

public class PickUpSystem : NetworkBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!isLocalPlayer) return;
        if(!collision.gameObject.CompareTag("ItemPickUpPlayer")) return;
        Item item = collision.GetComponent<Item>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
                CmdDestroyItem(item);
            else
                item.Quantity = reminder;
        }
    }

    [Command]
    public void CmdDestroyItem(Item item)
    {   
        item.DestroyItem();
    }
}
