using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItrmType {Default, Food, Weapon, Clothes, Quest, Materials, Other}
public class ItemScriptableObject : ScriptableObject
{
    public ItrmType itrmType;
    public string itemName;
    public int maximumAmount;
    public string itemDescription;
}
