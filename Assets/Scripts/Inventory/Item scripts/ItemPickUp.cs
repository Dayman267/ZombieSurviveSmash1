using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ItemPickUp : MonoBehaviour
{
    public InventoryItemData ItemData;
    //public Transform player;

    private Collider2D itemCollider;

    private void Awake()
    {
        itemCollider = GetComponent<Collider2D>();
        itemCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = collision.GetComponent<InventoryHolder>();
        if (!inventory) return; 

        if (inventory.InventorySystem.AddToInventory(ItemData, 1))
        {
            Destroy(this.gameObject);
        }
    }

   
}
