using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Inventory.Model;
using Inventory.UI;
using kcp2k;
using Mirror;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : NetworkBehaviour
    {
        private UIInventoryPage _inventoryUI;

        private QuickSlotUI consumableSlots;

        [SerializeField] private InventorySO inventoryData;

        public List<InventoryItem> initialItems = new List<InventoryItem>();

        [SerializeField] private AudioClip dropClip;

        [SerializeField] private AudioSource audioSource;


        private void Start()
        {
        
            if (!isLocalPlayer) return;
            _inventoryUI = GameObject.FindGameObjectWithTag("Inventory").GetComponent<UIInventoryPage>();
            _inventoryUI.Hide();
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if(item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }
        

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            _inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                _inventoryUI.UpdateData(item.Key,item.Value.item.ItemImage,item.Value.quanity);
            }
        }

        private void PrepareUI()
        {
            _inventoryUI.InitializeInventoryUI(inventoryData.Size);
            _inventoryUI.OnDescriptionRequested += HandleDescriptionRequest;
            _inventoryUI.OnSwapItems += HandleSwapItems;
            _inventoryUI.OnStartDragging += HandleDragging;
            _inventoryUI.OnItemActionRequested += HandleItemActionRequest;
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            
            if(inventoryItem.IsEmpty)
                return;

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                _inventoryUI.ShowItemAction(itemIndex);
                _inventoryUI.AddAction(itemAction.ActionName,() => PerformAction(itemIndex));
            }
            
            IDestroybleItem destroybleItem = inventoryItem.item as IDestroybleItem;
            if (destroybleItem != null)
            {
                _inventoryUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quanity));
            }
            
            IAddToQuickSlot itemForQuickSlot = inventoryItem.item as IAddToQuickSlot;
            if (itemForQuickSlot != null)
            {
                _inventoryUI.AddAction("AddToQuickSlot", () => AddItemToQuickSlot(itemIndex));
            }
            
        }

        private void DropItem(int itemIndex, int quanity)
        {
            inventoryData.RemoveItem(itemIndex, quanity);
            _inventoryUI.ResetSelection();
            audioSource.PlayOneShot(dropClip);
        }
        
        private void AddItemToQuickSlot(int itemIndex)
        {
            
           
            
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            
            if(inventoryItem.IsEmpty)
                return;
            
            IDestroybleItem destroybleItem = inventoryItem.item as IDestroybleItem;
            if (destroybleItem != null)
            {
                inventoryData.RemoveItem(itemIndex, 1);
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                itemAction.PerformAction(gameObject, inventoryItem.itemState);
                audioSource.PlayOneShot(itemAction.actionSFX);
                if(inventoryData.GetItemAt(itemIndex).IsEmpty)
                    _inventoryUI.ResetSelection();
            }
            
          //  IItemAction itemAction = inventoryItem.item as IItemAction;

        }

        private void HandleDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            
            if(inventoryItem.IsEmpty)
                return;
            
            _inventoryUI.CreatedDraggedItem(inventoryItem.item.ItemImage,inventoryItem.quanity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventoryData.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescriptionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
            if (inventoryItem.IsEmpty)
            {
                _inventoryUI.ResetSelection();
                return;
            }
           
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            _inventoryUI.UpdateDescription(itemIndex,item.ItemImage, item.name, description);
        }

        public string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(inventoryItem.item.Description);
            sb.AppendLine();
            
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                sb.Append($"{inventoryItem.itemState[i].itemParameter.ParameterName} : " +
                          $"{inventoryItem.itemState[i].value} / " +
                          $"{inventoryItem.item.DefaultParametersList[i].value}");
                sb.AppendLine();
            }

            return sb.ToString();
        }

        private void Update()
        {
            if (!isLocalPlayer) return;
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (_inventoryUI.isActiveAndEnabled == false)
                {
                    _inventoryUI.Show();
                    foreach (var item in inventoryData.GetCurrentInventoryState())
                    {
                        _inventoryUI.UpdateData(item.Key,
                            item.Value.item.ItemImage,
                            item.Value.quanity);
                    }
                }
                else
                {
                    _inventoryUI.Hide();
                }
            }
        }
    }
}