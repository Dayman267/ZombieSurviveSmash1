using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField]private List<InventorySlot2> inventorySlots;
    private int inventorySize;

    public List<InventorySlot2> InventorySlots =>  inventorySlots;
    public int InventorySize => InventorySlots.Count;
    public UnityAction<InventorySlot2> OnInventorySlotChanged;
    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot2>(size);
         for (int i = 0; i < size; i++) 
         {
              inventorySlots.Add(new InventorySlot2()); 
         }
    }
}
