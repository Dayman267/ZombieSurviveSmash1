using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Item : MonoBehaviour/*, IPointerClickHandler*/
{
    public ItemScriptableObject item;
    public int amount;
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("Clicked");
    //    if (InventoryManager.Triggered() == true)
    //    {
    //        Debug.Log("Taked");
    //        InventoryManager.AddItem(item, amount);
    //        Destroy(this);
    //    }

    //}
}
