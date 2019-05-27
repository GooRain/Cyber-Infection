using CyberInfection.Data.Entities.Unit;
using CyberInfection.Extension;
using CyberInfection.UI.Game;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;
using InventorySystem;
using System.Collections.Generic;

namespace CyberInfection.GameMechanics.Entity.Units
{
    public class Player : Unit
    {
        public static Player instance;
        public Inventory inventory;
        public List<string> blockers;
        public bool controlBlocked { get { return blockers.Count > 0; } }
        [SerializeField] private PlayerData _data;
        
        private int _maxHP;

        private PhotonView m_PhotonView;

        public float healthPercentage => (float) health / _maxHP;


        protected override void Awake()
        {
            base.Awake();
            inventory = GetComponent<Inventory>();
            m_PhotonView = GetComponent<PhotonView>();
            gameObject.tag = m_PhotonView.IsMine ? TagManager.PlayerTag : TagManager.OtherPlayerTag;

            health = _data.health;
            _maxHP = health;
            OnHealthChanged();
            
            UnitsManager.instance.OnPlayerSpawn(this);
        }

        public override void GetDamage(int damageAmount)
        {
            base.GetDamage(damageAmount);
            OnHealthChanged();
        }

        private void OnHealthChanged()
        {
            GameCanvas.instance.OnPlayerHealthChanged(healthPercentage);
        }

        public override void Die()
        {
            base.Die();
            UnitsManager.instance.OnPlayerDeath(this);
        }
    }
}
