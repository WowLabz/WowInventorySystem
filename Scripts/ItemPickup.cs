using System;
using UnityEngine;

namespace WowInventorySystem
{
    public class ItemPickup : MonoBehaviour
    {
        public static event Action<Item> OnInventoryItemPickup; 
        public Item item;

        private void Pickup()
        {
            OnInventoryItemPickup?.Invoke(item);
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Pickup();
        }
    }
}
