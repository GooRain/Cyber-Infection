using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "ItemAsset", menuName = "ItemsAsset")]
    public class ItemAsset : ScriptableObject
    {
        private static ItemAsset instance;
        public static ItemAsset Instance
        {
            get
            {
                if (instance == null)
                    instance = Resources.Load<ItemAsset>("ItemAsset");
                return instance;
            }
        }

        [SerializeField] private List<Item> items;

        public static Item GetItem(string id)
        {
            if (Instance.items.Exists(x => x.id == id))
            {
                Item item = Instance.items.Find(x => x.id == id);
                return item;
            }
            Debug.LogError($"Item {id} does not exist in ItemAsset!");
            return null;
        }

        public static string[] GetAllIds()
        {
            string[] ids = new string[Instance.items.Count];
            for (int i = 0; i < ids.Length; i++)
                ids[i] = Instance.items[i].id;
            return ids;
        }
    }
}