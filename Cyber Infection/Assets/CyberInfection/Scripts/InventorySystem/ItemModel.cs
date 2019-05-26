using System;
using System.Collections;
using CyberInfection.GameMechanics.Entity.Units;
using UnityEngine;

namespace InventorySystem
{
    public class ItemModel : MonoBehaviour
    {
        public string itemId;
        public bool isQuestItem = false;
        public AudioSource source;
        public AudioClip pickUpSound;
        public Transform activator;
        [HideInInspector] public Item item;

        public int GetLayer()
        {
            return gameObject.layer;
        }

        public Transform GetPivot()
        {
            return transform;
        }

        public Transform GetPoint()
        {
            return activator;
        }

        public void PickUp()
        {
            if (Player.instance.inventory.HasItem(item.id)) return;
            Player.instance.inventory.AddItem(item);
            if (pickUpSound)
                source.PlayOneShot(pickUpSound);
            Destroy(gameObject);
            if (isQuestItem)
            {
                StartCoroutine(activating());
            }
        }

        IEnumerator activating()
        {
            yield return new WaitForSeconds(0.7f);
            if (!Player.instance.blockers.Contains("itemModel"))
                Player.instance.blockers.Add("itemModel");
            CyberInfection.UI.Game.GameCanvas.instance.newItemWindow.ShowQuestItem(itemId);
        }

        private void OnWindowClosed(string eventId)
        {
            Player.instance.blockers.Remove("itemModel");
        }

        private void Start()
        {
            item = ItemAsset.GetItem(itemId);
        }
    }
}