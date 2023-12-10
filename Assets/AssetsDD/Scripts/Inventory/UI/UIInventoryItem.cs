using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
   public class UIInventoryItem : MonoBehaviour, IPointerClickHandler,IBeginDragHandler,IEndDragHandler,IDropHandler,IDragHandler
   {
      [SerializeField] private Image _itemImage;
      [SerializeField] private TMP_Text _quanityTxt;

      [SerializeField] private Image _borderImage;
   
      private bool _empty = true;
   
      public event Action<UIInventoryItem> OnItemClicked, OnItemDeroppedOn, OnItemBeginDrag,
         OnItemEndDrag, OnRightMouseBtnClick;
   
      public void Awake()
      {
         ResetData();
         Deselect();
      }

      public void Deselect()
      {
         _borderImage.enabled = false;
      }

      public void ResetData()
      {
         _itemImage.gameObject.SetActive(false);
         _empty = true;
      }

      public void SetData(Sprite sprite, int quanity)
      {
         _itemImage.gameObject.SetActive(true);
         _itemImage.sprite = sprite;
         _quanityTxt.text = quanity + "";
         _empty = false;
      }

      public void Select()
      {
         _borderImage.enabled = true;
      }
   
      public void OnPointerClick(PointerEventData pointerData)
      {
         if(pointerData.button == PointerEventData.InputButton.Right)
            OnRightMouseBtnClick?.Invoke(this);
         else
            OnItemClicked?.Invoke(this);
      }

      public void OnBeginDrag(PointerEventData eventData)
      {
         if(_empty)
            return;
         OnItemBeginDrag?.Invoke(this);
      }

      public void OnEndDrag(PointerEventData eventData)
      {
         OnItemEndDrag?.Invoke(this);
      }

      public void OnDrop(PointerEventData eventData)
      {
         OnItemDeroppedOn?.Invoke(this);
      }

      public void OnDrag(PointerEventData eventData)
      {
     
      }
   }
}