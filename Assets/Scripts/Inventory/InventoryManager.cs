using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    public GameObject UIBG;
    public Transform RandomChestPanel;
    public Transform inventoryPanel;
    public List<InventorySlot1> chestSlots = new List<InventorySlot1>();
    [SerializeField] private Transform firePoint;
    public List<InventorySlot1> slots = new List<InventorySlot1>();
    public bool isOpened;
    private Camera mainCamera;
    [SerializeField, Range(0, 5)] private float maxDistance = 1;
    public Collider2D collider;
   
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot1>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot1>());
            }
        }
        for (int i = 0; i < RandomChestPanel.childCount; i++)
        {
            if (RandomChestPanel.GetChild(i).GetComponent<InventorySlot1>() != null)
            {
                chestSlots.Add(RandomChestPanel.GetChild(i).GetComponent<InventorySlot1>());
            }
        }
        UnactiveUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpened = !isOpened;
            if (isOpened)
            {
                UIBG.SetActive(true);
                inventoryPanel.gameObject.SetActive(true);
            }
            else
            {
                UIBG.SetActive(false);
                inventoryPanel.gameObject.SetActive(false);
            }
            //RandomChestPanel.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClearPanel(chestSlots);
            UnactiveUI();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            GrabItem();
        }
    }
    private Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
    private void GrabItem()
    {

        if (Physics2D.Raycast(firePoint.position, ScreenToWorld(mainCamera, Input.mousePosition) - firePoint.position, maxDistance))
        {
            
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, ScreenToWorld(mainCamera, Input.mousePosition) - firePoint.position, maxDistance);
            if (_hit.collider.gameObject.GetComponent<Item>() != null)
            {
                AddItem(_hit.collider.gameObject.GetComponent<Item>().item, _hit.collider.gameObject.GetComponent<Item>().amount,slots);
                Destroy(_hit.collider.gameObject);
            }
            else if (_hit.collider.gameObject.GetComponent<Chest>() != null) 
            {
                RandomChestPanel.gameObject.SetActive(true);
                _hit.collider.gameObject.GetComponent<Chest>().isOpened = true;
                for(int i = 0; i < _hit.collider.gameObject.GetComponent<Chest>().chestItems.Count; i++)
                {
                    AddItem(_hit.collider.gameObject.GetComponent<Chest>().chestItems[i],1, chestSlots);
                    
                }
                
            }
            
        }


    }

    public void AddItem(ItemScriptableObject _item, int _amount, List<InventorySlot1> inventoryPanelSlots)
    {
        foreach (InventorySlot1 slot in inventoryPanelSlots)
        {
           
            if (slot.item == _item)
            {
                if (slot.amount + _amount <= _item.maximumAmount) 
                {
                    slot.amount += _amount;
                    slot.itemAmountText.text = slot.amount.ToString();
                    return;
                }
                break;
            }
                
        }
        foreach (InventorySlot1 slot in inventoryPanelSlots)
        {
            if (slot.isEmpty == true)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
                slot.SetIcon(_item.icon);
                slot.itemAmountText.text = _amount.ToString();
                break;
            }
        }
    }

    public void ClearPanel(List<InventorySlot1> inventoryPanelSlots)
    {
        foreach (InventorySlot1 slot in inventoryPanelSlots)
        {
            slot.item = null;
            slot.amount = 0;
            slot.isEmpty = true;
            slot.iconGO = null;
            slot.itemAmountText = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, maxDistance);
    }

    private void UnactiveUI() 
    {
        RandomChestPanel.gameObject.SetActive(false);
        UIBG.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);
    }


}
