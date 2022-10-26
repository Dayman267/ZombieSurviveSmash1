using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FoodItem", menuName = "Inventory/Items/New Food Item")]
public class FoodItem : ItemScriptableObject
{
    public float healAmount;
    private void Start() {
        itemType = ItemType.Food;
    }
}
