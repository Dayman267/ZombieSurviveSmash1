using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    public enum Types {Default, Weapon, Consumable, Pattern, SuitMod, Resource}
    public abstract class ItemSO : ScriptableObject
    {
        public int ID => GetInstanceID();
    
        [field: SerializeField] public string Name { get; set; }
        [field: SerializeField] public Types Type { get; set; } = Types.Default;
        [field: SerializeField] public Sprite ItemImage { get; set; }

        [field: SerializeField]
        [field: TextArea]
        public string Description { get; set; } = "Some interesting item";
    
        [field: SerializeField] public bool IsStackable { get; set; }
        [field: SerializeField] public bool IsSaveable { get; set; }
        [field: SerializeField] public int MaxStackSize { get; set; } = 1;
        
        [field: SerializeField] public List<ItemParameter> DefaultParametersList { get; set; }
    }

    [Serializable]
    public struct ItemParameter : IEquatable<ItemParameter>
    {
        public ItemParameterSO itemParameter;
        public float value;
        
        public bool Equals(ItemParameter other)
        {
            return other.itemParameter == itemParameter;
        }
    }
}
