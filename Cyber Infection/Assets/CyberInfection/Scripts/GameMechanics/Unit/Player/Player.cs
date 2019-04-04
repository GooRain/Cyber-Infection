using CyberInfection.Data.Unit;
using CyberInfection.Extension;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace CyberInfection.GameMechanics.Unit.Player
{
    public class Player : Unit
    {
        [SerializeField] private PlayerData _data;
        [SerializeField] private Image _healthBar;
        private float _lastHP, _startHealth;

        private PhotonView m_PhotonView;

        protected override void Awake()
        {
            base.Awake();

            m_PhotonView = GetComponent<PhotonView>();
            gameObject.tag = m_PhotonView.IsMine ? TagManager.PlayerTag : TagManager.OtherPlayerTag;

            health = _data.health;
        }

        private void Start()
        {
            _startHealth = health;
            _healthBar.fillAmount = 1;
            _lastHP = health;
        }

        private void Update()
        {
            
            if (health != _lastHP)
            {
                Debug.Log(_healthBar.fillAmount);
                _healthBar.fillAmount = health / _startHealth;
                _lastHP = health;
            }
        }
            
    }
}
