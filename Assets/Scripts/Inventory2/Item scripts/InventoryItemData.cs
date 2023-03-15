using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this is a scriptable object,that defines what an item is in our game.
//It could be inherited from to have branched version of items, for example potions and equipment.


[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int ID;
    public string DisplayName;
    [TextArea(4,4)]
    public string Description; 
    public Sprite Icon;
    public int maxStackSize;
    public int itemPrice;
}
