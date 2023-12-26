using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class QuickSlotUI : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler,
    IDragHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _quanityTxt;
    [SerializeField] private GameObject txtBorder;
    


    private bool _empty = true;

    public event Action<QuickSlotUI> OnItemClicked,
        OnItemDeroppedOn,
        OnItemBeginDrag,
        OnItemEndDrag,
        OnRightMouseBtnClick;

    public void Awake()
    {
        Initialize();
        ResetData();
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

    private void Initialize()
    {
         _itemImage.gameObject.SetActive(false);
         txtBorder.gameObject.SetActive(false);
         _quanityTxt.text = "";
         
    }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if(pointerData.button == PointerEventData.InputButton.Left)
            OnItemClicked?.Invoke(this);
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnDrag(PointerEventData eventData)
    {
        throw new NotImplementedException();
    }
}