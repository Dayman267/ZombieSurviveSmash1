using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class MouseFollower : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Camera mainCam;
        [SerializeField] private UIInventoryItem item;

        private void Awake()
        {
            canvas = transform.root.GetComponent<Canvas>();
            mainCam = Camera.main;
            item = GetComponentInChildren<UIInventoryItem>();
        }

        public void SetData(Sprite sprite, int quanity)
        {
            item.SetData(sprite,quanity);    
        }


        private void Update()
        {
            Vector2 position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)canvas.transform,
                Input.mousePosition,
                canvas.worldCamera,
                out position);
            transform.position = canvas.transform.TransformPoint(position);
        }

        public void Toggle(bool val)
        {
            Debug.Log($"Item toggled {val}");
            gameObject.SetActive(val);
        }
    }
}