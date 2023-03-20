using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay inventoryPanel;
    private RectTransform UI_ControllerSize;
    private bool panelIsOpend = false;

    private void Awake()
    {
        //Screen.currentResolution resolutions = ShimManager.screenShim.currentResolution;
        UI_ControllerSize = this.GetComponent<RectTransform>();
        UI_ControllerSize.sizeDelta = new Vector2(Display.main.systemWidth, Display.main.systemHeight);
        inventoryPanel.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    void Update()
    {
        //if (Keyboard.current.tabKey.wasPressedThisFrame)
        //{
        //    if (!panelIsOpend)
        //    {
        //        panelIsOpend = true;
        //        DisplayInventory(new InventorySystem(9));
        //    }
        //    else
        //    {
        //        panelIsOpend = false;
        //        inventoryPanel.gameObject.SetActive(false);
        //    }
        //}

        if(inventoryPanel.gameObject.activeInHierarchy && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            panelIsOpend = false;
            inventoryPanel.gameObject.SetActive(false);
        }
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        inventoryPanel.gameObject.SetActive(true);
        inventoryPanel.RefreshDynamicInventory(invToDisplay);
    }
}
