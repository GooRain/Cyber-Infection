using CyberInfection.Data.Unit;
using CyberInfection.Extension;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection.GameMechanics.Unit.Player
{
	public class Player : Unit
    {
        [SerializeField] private PlayerData _data;
        
        private PhotonView m_PhotonView;

        protected override void Awake()
        {
            base.Awake();

            m_PhotonView = GetComponent<PhotonView>();
            gameObject.tag = m_PhotonView.IsMine ? TagManager.PlayerTag : TagManager.OtherPlayerTag;
            
            health = _data.health;
        }
    }
}
