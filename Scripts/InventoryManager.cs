using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace WowInventorySystem
{
    public class InventoryManager : MonoBehaviour
    {
        public static event Action<bool> OnInventoryRequested;
        public static event Action OnItemAddedToInventory;
        public static event Action OnItemRemovedFromInventory;

        //Setting up the custom inputs to be read here.
        private CustomPlayerInputs _customPlayerInputs;
        
        //Create a new list so we can add item pickups as inventory items.
        public List<Item> playerInventory = new List<Item>();
        
        private void OnEnable()
        {
            ItemPickup.OnInventoryItemPickup += HandleItemPickup; // Listen to items being picked up and call add item
            CustomPlayerInputs.OnRequestToggleInventoryCanvas += RequestInventory; // Listen to inventory toggle requests and pass on message
            
        }

        private void OnDisable()
        {
            ItemPickup.OnInventoryItemPickup -= HandleItemPickup; // Stop listening to inventory toggle requests and pass on message
            CustomPlayerInputs.OnRequestToggleInventoryCanvas -= RequestInventory; // Stop listening to inventory toggle requests and pass on message
        }

        private void HandleItemPickup(Item item)
        {
            AddItem(item);
            UpdatePlayerInventory();
            OnItemAddedToInventory?.Invoke();
        }
        
        private void HandleItemDrop(Item item)
        {
            RemoveItem(item);
            UpdatePlayerInventory();
            OnItemRemovedFromInventory?.Invoke();
        }
        private static void RequestInventory(bool inventoryState) => OnInventoryRequested?.Invoke(inventoryState);
        public void UpdatePlayerInventory() => Variables.Application.Set("PlayerInventory", playerInventory);
        
        private void AddItem(Item item) => playerInventory.Add(item);
        private void RemoveItem(Item item) => playerInventory.Remove(item);
        
    }
}
