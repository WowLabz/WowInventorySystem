using UnityEngine;
using UnityEngine.Serialization;


namespace WowInventorySystem
{
    [CreateAssetMenu(fileName = "New Inventory Item", menuName = "WowInventorySystem / New Inventory Item")]
    public class Item : ScriptableObject
    {
        public int itemId;
        public string itemName;
        public int value;
        public Sprite icon;

    }
}
