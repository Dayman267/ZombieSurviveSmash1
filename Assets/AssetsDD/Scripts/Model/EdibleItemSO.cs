using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroybleItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();

        public string ActionName => "Consume";

        [field: SerializeField] public AudioClip actionSFX { get; private set;}
        public bool PerformAction(GameObject character, List<ItemParameter> itemState = null)
        {
            foreach (ModifierData data in modifierData)
            {
                data.statModifier.AffectCharacter(character,data.value);
            }
            return true;
        }
    }

    public interface IDestroybleItem
    {
        
    }

    public interface IItemAction
    {
        public string ActionName { get; }
        public AudioClip actionSFX { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterStatModifierSO statModifier;
        public float value;
    }
}