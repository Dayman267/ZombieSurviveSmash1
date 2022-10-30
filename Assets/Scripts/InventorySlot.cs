using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public ItemScriptableObject item;
    public int amount;
    public bool isEmpty = true;
    public GameObject iconeGo;
    public TMP_Text itemAmountText;

    private void Start()
    {
        iconeGo = transform.GetChild(0).gameObject;
        itemAmountText = transform.GetChild(1).GetComponent<TMP_Text>();
    }
    public void SetIcon(Sprite icon) 
    {
        iconeGo.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        iconeGo.GetComponent<Image>().sprite = icon;
    }


}
