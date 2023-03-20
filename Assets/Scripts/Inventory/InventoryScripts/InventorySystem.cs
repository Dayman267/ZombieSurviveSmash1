using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

[System.Serializable]
public class InventorySystem
{
    [SerializeField]private List<InventorySlot> inventorySlots;
    private int inventorySize;

    public List<InventorySlot> InventorySlots =>  inventorySlots;
    public int InventorySize => InventorySlots.Count;

    public UnityAction<InventorySlot> OnInventorySlotChanged;

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size);
         
         for (int i = 0; i < size; i++) 
         {
              inventorySlots.Add(new InventorySlot()); 
         }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        if(ContainsItem(itemToAdd, out List<InventorySlot> invSlots)) // Check wether item exist in inventory
        {
            foreach(var slot in invSlots)
            {
                if (slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd);
                    OnInventorySlotChanged?.Invoke(slot);
                    return true;
                }
            }
            
        }
        
        if (HasFreeSlot(out InventorySlot freeSlot)) // Gets the first avilable slot 
        {
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
            //Add implamentation to only take what can fill the stack,and check for another free slot to put the remainder in.
        }
        return false;
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlot)//Do any of our slots have the item to add in them?  
    {
        invSlot = inventorySlots.Where(i => i.ItemData == itemToAdd).ToList();//If they do,the get a list of allof them
        return invSlot == null ? false : true; // If they do return true, if not return false.
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(i => i.ItemData == null); // Gets the first free slot 
        return freeSlot == null ? false : true;
    }
}
