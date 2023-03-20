using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType {Food, Weapon, All}
public enum ChestLevel {lvl1 = 10, lvl2 = 20, lvl3 = 30, lvl4 = 40, lvl5 = 50, lvl6 = 60, lvl7 = 70, lvl8 = 80, lvl9 = 90, lvl10 = 100}

[CreateAssetMenu(menuName = "Inventory System/LootPattern")]
public class LootManager : ScriptableObject
{
    public ItemType itemType;
    public ChestLevel level;
}
