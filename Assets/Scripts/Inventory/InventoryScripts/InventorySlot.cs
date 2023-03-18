using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData; //Reference to he data 
    [SerializeField] private int stackSize; // curent stack size - how many of the data do we have? 

    public InventoryItemData ItemData => itemData; 
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount)  
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() 
    {
        ClearSlot();
    }

    public void ClearSlot() // clears the slot 
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) // assigns an item to the slot 
    {
        if (itemData == invSlot.ItemData) AddToStack(invSlot.StackSize); //Does  the slot contain the same item? Add to the stack if so.
        else //vveride slot with the inventory slot that were passing in.
        {
            itemData = invSlot.ItemData;
            stackSize = 0;
            AddToStack(invSlot.StackSize);
        }
        
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount)//Update slot directly.
    {
        itemData = data;
        stackSize = amount;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public bool EnoughRoomLeftInStack(int amountToAdd, out int amountRemaining) //Would there be enough room in the stack for the amount were tryinmg to add.
    {
        amountRemaining = itemData.maxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd);
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if(itemData == null || itemData != null && stackSize + amountToAdd <= itemData.maxStackSize)
            return true;
        else return false;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if(stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(itemData, halfStack);
        return true;
    }

    public bool TakeOneFromStack(out InventorySlot oneItem)
    {
        if (stackSize <= 1)
        {
            oneItem = null;
            return false;
        }
        RemoveFromStack(1);

        oneItem = new InventorySlot(itemData, 1);
        return true;
    }
}
