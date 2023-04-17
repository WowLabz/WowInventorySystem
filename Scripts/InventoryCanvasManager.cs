using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace WowInventorySystem
{
    public class InventoryCanvasManager : MonoBehaviour
    {
        private Canvas _canvas;
        private List<Item> _inventory;
        [SerializeField] private GameObject itemPrefab;
        [SerializeField] private Transform itemTransform;
        private void Awake() => _canvas = GetComponent<Canvas>();
        private void Start() => HandleInventoryCanvasDisplay(false);
        private void OnEnable()
        {
            InventoryManager.OnInventoryRequested += HandleInventoryCanvasDisplay;
        }

        private void OnDisable()
        {
            InventoryManager.OnInventoryRequested -= HandleInventoryCanvasDisplay;
        }

        private void HandleInventoryCanvasDisplay(bool activationStatus)
        {
            switch (activationStatus)
            {
                case true:
                    ReadInventoryItems();
                    Debug.Log("Case is true");
                    break;
                case false:
                    ClearInventoryCanvas();
                    Debug.Log("Case is false");
                    break;
            }
            _canvas.enabled = activationStatus;
        }

        public void ReadInventoryItems()
        {
            _inventory = (List<Item>)Variables.Application.Get("PlayerInventory");
            
            if (_inventory == null) return; //Don't do anything on an empty inventory

            foreach (var item in _inventory)
            {
                var itemIcon = itemPrefab.transform.Find("img_ItemIcon").GetComponent<Image>();
                var itemName = itemPrefab.transform.Find("t_ItemName").GetComponentInChildren<TextMeshProUGUI>();
                
                itemIcon.sprite = item.icon;
                itemName.SetText(item.itemName);

                if (itemPrefab != null && itemTransform != null)
                {
                    var go = Instantiate(itemPrefab, itemTransform);
                    go.tag = "InventoryItem";
                    go.name = item.itemName;
                    Debug.Log(item.itemName);
                }
                else
                {
                    Debug.Log("Sorry, no items found");
                }
            }
        }

        private static void ClearInventoryCanvas()
        {
            var instantiatedInventoryItems = GameObject.FindGameObjectsWithTag("InventoryItem");

            // Iterate through the array of instantiated objects and destroy them
            foreach (var obj in instantiatedInventoryItems) Destroy(obj);
        }
    }
}
