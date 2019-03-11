using CyberInfection.Extension;
using Photon.Pun;

namespace CyberInfection.GameMechanics.Unit.Player
{
	public class Player : Unit
	{
        private PhotonView m_PhotonView;

        protected override void Awake()
        {
            base.Awake();

            m_PhotonView = GetComponent<PhotonView>();
            gameObject.tag = m_PhotonView.IsMine ? TagManager.PlayerTag : TagManager.OtherPlayerTag;
        }

        private void Start()
        {
            health = 100;
        }
    }
}
