using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chest : MonoBehaviour
{
    
    public bool isOpened = false;
    public List<ItemScriptableObject> chestItems = new List<ItemScriptableObject>(9);
    public ItemScriptableObject[] items;
    private void Start()
    {
        RandomItemGeneration();
    }
    private void RandomItemGeneration()
    {
        int j = 0;
        for (int i = 0; i < Random.Range(1, 10); i++)
        {
            chestItems[j] = items[Random.Range(0, items.Length)];
            j++;
        }
        
    }

}
