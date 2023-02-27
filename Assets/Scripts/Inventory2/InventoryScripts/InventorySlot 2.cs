using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot2
{
    [SerializeField] private InventoryItemData itemData;
    [SerializeField] private int stackSize;

    public InventoryItemData Data => itemData;
    public int StackSize => stackSize;

    public InventorySlot2(InventoryItemData source, int amount)
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot2()
    {
        ClearSlot();
    }

    public void ClearSlot()
    {
        itemData = null;
        stackSize = -1;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) 
    {
        amountRemaining = itemData.maxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd);
    }

    public bool RoomLeftInStack(int amountToAdd) 
    {
        if(stackSize + amountToAdd <= itemData.maxStackSize)
            return true;
        else return false;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
}
