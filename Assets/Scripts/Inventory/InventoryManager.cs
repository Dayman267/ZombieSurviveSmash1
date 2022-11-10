using System.Collections.Generic;
using UnityEngine;
public class InventoryManager : MonoBehaviour
{
    public GameObject UIBG;
    public Transform inventoryPanel;
    [SerializeField] private Transform firePoint;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened;
    private Camera mainCamera;
    [SerializeField, Range(0, 5)] private float maxDistance = 1;
    public Collider2D collider;
    //private void Awake()
    //{
    //    UIPanel.SetActive(true);
    //}
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        mainCamera = Camera.main;
        for (int i = 0; i < inventoryPanel.childCount; i++)
        {
            if (inventoryPanel.GetChild(i).GetComponent<InventorySlot>() != null)
            {
                slots.Add(inventoryPanel.GetChild(i).GetComponent<InventorySlot>());
            }
        }
        UIBG.SetActive(false);
        inventoryPanel.gameObject.SetActive(false);
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

        Debug.DrawRay(firePoint.position, ScreenToWorld(mainCamera, Input.mousePosition) - firePoint.position, Color.red);
        if (Physics2D.Raycast(firePoint.position, ScreenToWorld(mainCamera, Input.mousePosition) - firePoint.position, maxDistance, LayerMask.GetMask("Objects")))
        {
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, ScreenToWorld(mainCamera, Input.mousePosition) - firePoint.position, maxDistance, LayerMask.GetMask("Objects"));
            if (_hit.collider.gameObject.GetComponent<Item>() != null)
            {
                Debug.Log("jfjfjfj" + _hit.collider.gameObject);
                AddItem(_hit.collider.gameObject.GetComponent<Item>().item, _hit.collider.gameObject.GetComponent<Item>().amount);
                Destroy(_hit.collider.gameObject);
            }
        }


    }
    //}

    //public static bool Triggered() 
    //{

    //        Vector2 firePointToMousePoint = mainCamera.ScreenToWorldPoint(Input.mousePosition) - firePoint.position;
    //        Debug.Log(Mathf.Abs((mainCamera.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).magnitude));
    //        if (Mathf.Abs(firePointToMousePoint.magnitude) <= maxDistance) 
    //        {
    //            return true;
    //        }
    //        else return false; 
    //}
    public void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
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
        foreach (InventorySlot slot in slots)
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, maxDistance);
    }


}
