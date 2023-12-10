using System;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemActionPanel : MonoBehaviour
    {
        [SerializeField] private GameObject buttonPrefab;

        public void AddButton([CanBeNull] string name, Action onClickAction)
        {
            GameObject button = Instantiate(buttonPrefab, transform);
            button.GetComponent<Button>().onClick.AddListener(() => onClickAction());
            button.GetComponentInChildren<TMPro.TMP_Text>().text = name;
        }

        public void Toggle(bool val)
        {
            if (val = true)
                RemoveOldButtons();
            gameObject.SetActive(val);
        }

        public void RemoveOldButtons()
        {
            foreach (Transform transformChildObject in transform)
            {
                Destroy(transformChildObject.gameObject);
            }
        }
    }
}