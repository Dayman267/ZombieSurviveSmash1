using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject UIPanel;
    public Transform inventoryPanel;
    [SerializeField] private Transform firePoint;
    public List<InventorySlot> slots = new List<InventorySlot>();
    public bool isOpened;
    private Camera mainCamera;
    public float reachDistance = 3;
    [SerializeField] private float maxDistance = 3;
    public LayerMask allowGrab;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
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
        if (Input.GetKeyDown(KeyCode.Mouse0)) GrabItem();
        //Vector2 ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        //RaycastHit2D hit;
        //if (Physics2D.Raycast(ray, out hit))
        //{
        //    Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.green);
        //    if (hit.collider.gameObject.GetComponent<Item>() != null)
        //    {
        //        AddItem(hit.collider.gameObject.GetComponent<Item>().item, hit.collider.gameObject.GetComponent<Item>().amount);
        //        Destroy(hit.collider.gameObject.GetComponent<Item>());
        //    }
        //}
        //else { Debug.DrawRay(ray.origin, ray.direction * reachDistance, Color.red); }
    }
    private Vector3 ScreenToWorld(Camera camera, Vector3 position)
    {
        position.z = camera.nearClipPlane;
        return camera.ScreenToWorldPoint(position);
    }
    private void GrabItem()
    {
       
        Debug.DrawRay(firePoint.position, ScreenToWorld(mainCamera, Input.mousePosition) - firePoint.position ,Color.red);
        if (Physics2D.Raycast(firePoint.position, ScreenToWorld(mainCamera, Input.mousePosition) - firePoint.position, maxDistance, allowGrab))
        {
            Destroy(this.gameObject);
            RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, ScreenToWorld(mainCamera, Input.mousePosition) - firePoint.position, maxDistance, allowGrab);
            if (_hit.collider.gameObject.GetComponent<Item>() != null)
            {
                AddItem(_hit.collider.gameObject.GetComponent<Item>().item, _hit.collider.gameObject.GetComponent<Item>().amount);
                Destroy(_hit.collider.gameObject.GetComponent<Item>());
            }
        }
    }
    private void AddItem(ItemScriptableObject _item, int _amount)
    {
        foreach (InventorySlot slot in slots)
        {
            if (slot.item == _item)
            {
                slot.amount += _amount;
                return;
            }
        }
        foreach (InventorySlot slot in slots)
        {
            if (slot.isEmpty == false)
            {
                slot.item = _item;
                slot.amount = _amount;
                slot.isEmpty = false;
            }
        }
    }
}
