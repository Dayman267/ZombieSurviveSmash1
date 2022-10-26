using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject UIPanel;
    public Transform inventoryPanel;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventoryPanel.childCount; i++) 
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null) 
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        UIPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) 
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIPanel.SetActive(true);
            }
            else 
            {
                UIPanel.SetActive(false);
            }
        }
    }
}
