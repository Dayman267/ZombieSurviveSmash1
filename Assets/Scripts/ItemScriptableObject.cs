using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {Default, Food, Weapon, Clothes, Quest, Materials, Other}
public class ItemScriptableObject : ScriptableObject
{
    public ItemType itemType;
    public Sprite icon;
    public GameObject itemPrefab;
    public string itemName;
    public int maximumAmount;
    public string itemDescription;
}
