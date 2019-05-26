using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        public List<Item> items;

        public System.EventHandler<InventoryEventArgs> OnItemAdded;
        public System.EventHandler<InventoryEventArgs> OnItemRemoved;
        private InventoryEventArgs args;

        private void Start()
        {
            items = new List<Item>();
        }

        public void AddItem(Item item)
        {
            if (HasItem(item.id))
            {
                Debug.LogError($"Item [{item.id}] is already in inventory");
                return;
            }
            items.Add(item);
            args = new InventoryEventArgs(item.id);
            OnItemAdded?.Invoke(this, args);
        }

        public void AddItem(string id)
        {
            var item = ItemAsset.GetItem(id);
            AddItem(item);
        }

        public void RemoveItem(string id)
        {
            if (HasItem(id))
                items.RemoveAll(x => x.id == id);
            args = new InventoryEventArgs(id);
            OnItemRemoved?.Invoke(this, args);
        }

        public bool HasItem(string id)
        {
            return items.Exists(x => x.id == id);
        }

        public List<string> GetList()
        {
            List<string> s = new List<string>();
            foreach (var v in items)
                s.Add(v.id);
            return s;
        }

        public void AddItems(List<string> items)
        {
            for(int i = 0; i < items.Count; i++)
            {
                AddItem(items[i]);
            }
        }
    }

    [System.Serializable]
    public class Item
    {
        public string id;
        public string name;
        public Sprite icon;

        public Item(string id)
        {
            Item item = ItemAsset.GetItem(id);
            this.id = id;
            this.name = item.name;
            this.icon = item.icon;
        }
    }

    public class InventoryEventArgs : System.EventArgs
    {
        private string id;
        public string Id { get { return id; } }

        public InventoryEventArgs(string id)
        {
            this.id = id;
        }
    }
}