using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] private MouseItemData mouseInventoryItem;
    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary; // Pair up the UI slots with the system slots.
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {
    }

    public abstract void AssignSlot(InventorySystem invToDisplay); //Implemented in child classes.

    protected virtual void UpdateSlot(InventorySlot updatedSlot)
    {
        foreach (var slot in SlotDictionary)
            if (slot.Value == updatedSlot) // slot value  - the "under the hood" inventory slot .
                slot.Key.UpdateUISlot(updatedSlot); // Slot Key - the UI representation of the value.
    }

    public void SlotClick(InventorySlot_UI clickedUISlot)
    {
        var isShiftPressed = Keyboard.current.leftShiftKey.isPressed;
        var isAltClickPressed = Keyboard.current.leftAltKey.isPressed;
        //bool isAltClickPressed = Mouse.current.rightButton.wasPressedThisFrame;
        //Ckicked slot has an item - mouse doesn't have a item - pick up that item

        if (clickedUISlot.AssignedInventorySlot.ItemData != null &&
            mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            //If the player is holding shift key? Split the stack.
            if (isShiftPressed && clickedUISlot.AssignedInventorySlot.SplitStack(out var halfStackSlot)) //split stack
            {
                mouseInventoryItem.UpdateMouseSlot(halfStackSlot);
                clickedUISlot.UpdateUISlot();
                return;
            }

            if (isAltClickPressed &&
                clickedUISlot.AssignedInventorySlot.TakeOneFromStack(out var oneItem)) //split stack
            {
                mouseInventoryItem.UpdateMouseSlot(oneItem);
                clickedUISlot.UpdateUISlot();
                return;
            }

            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
            // mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            // clickedUISlot.ClearSlot();
            // return;
        }

        if (clickedUISlot.AssignedInventorySlot.ItemData != null &&
            mouseInventoryItem.AssignedInventorySlot.ItemData == null)
        {
            //If the player is holding right key? Split the stack.
        }

        //Clocked slot doesn't have a item - mouse doesn't have a item - place the mouse item into the empty slot.
        if (clickedUISlot.AssignedInventorySlot.ItemData == null &&
            mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
            clickedUISlot.UpdateUISlot();

            mouseInventoryItem.ClearSlot();
            return;
        }


        //Are both items the same? If so combine them.
        //IS the slot stack size + mouse stack size > the slot max stack size ? If so, take from mouse.
        //If different items? then swap the items

        //Both slot have an item - decide what to do
        if (clickedUISlot.AssignedInventorySlot.ItemData != null &&
            mouseInventoryItem.AssignedInventorySlot.ItemData != null)
        {
            var isSameItem = clickedUISlot.AssignedInventorySlot.ItemData ==
                             mouseInventoryItem.AssignedInventorySlot.ItemData;

            if (isSameItem &&
                clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(mouseInventoryItem.AssignedInventorySlot
                    .StackSize))
            {
                clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.AssignedInventorySlot);
                clickedUISlot.UpdateUISlot();

                mouseInventoryItem.ClearSlot();
                return;
            }

            if (isSameItem &&
                !clickedUISlot.AssignedInventorySlot.EnoughRoomLeftInStack(
                    mouseInventoryItem.AssignedInventorySlot.StackSize, out var leftInStack))
            {
                if (leftInStack < 1)
                {
                    SwapSlots(clickedUISlot); //Stack is  full so swap the items.
                }
                else // Slot is not a max, so take whats needed from the mouse inventory.
                {
                    var remainingOnMouse = mouseInventoryItem.AssignedInventorySlot.StackSize - leftInStack;
                    clickedUISlot.AssignedInventorySlot.AddToStack(leftInStack);
                    clickedUISlot.UpdateUISlot();

                    var newItem = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData,
                        remainingOnMouse);
                    mouseInventoryItem.ClearSlot();
                    mouseInventoryItem.UpdateMouseSlot(newItem);
                }
            }
            else if (!isSameItem)
            {
                SwapSlots(clickedUISlot);
            }
        }
    }

    private void SwapSlots(InventorySlot_UI clickedUISlot)
    {
        var clonedSlot = new InventorySlot(mouseInventoryItem.AssignedInventorySlot.ItemData,
            mouseInventoryItem.AssignedInventorySlot.StackSize);
        mouseInventoryItem.ClearSlot();
        mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);

        clickedUISlot.ClearSlot();
        clickedUISlot.AssignedInventorySlot.AssignItem(clonedSlot);
        clickedUISlot.UpdateUISlot();
    }
}