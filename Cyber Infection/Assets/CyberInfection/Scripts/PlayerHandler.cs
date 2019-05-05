using System.Collections.Generic;
using Boo.Lang;
using CyberInfection.GameMechanics.Camera;
using CyberInfection.GameMechanics.Entity.Player;
using Photon.Pun;
using UnityEngine;

namespace CyberInfection
{
    [System.Serializable]
    public class PlayerHandler
    {
        [SerializeField] private Player m_PlayerPrefab;

        private Player m_LocalPlayer;

        private Dictionary<string, Player> m_PlayersDictionary = new Dictionary<string, Player>();

        public Player player => m_LocalPlayer;
        
        public void RefreshMinePlayer()
        {
            RefreshInstance(ref m_LocalPlayer, m_PlayerPrefab);

            if (CameraController.instance != null)
            {
                CameraController.instance.SetPlayer(m_LocalPlayer);
            }
        }

        public void RefreshOtherPlayer(string newPlayerUserId)
        {
            if (m_PlayersDictionary.ContainsKey(newPlayerUserId))
            {
                var refreshingPlayer = m_PlayersDictionary[newPlayerUserId];
                RefreshInstance(ref refreshingPlayer, m_PlayerPrefab);
                return;
            }

            var newPlayer = new GameObject().AddComponent<Player>();
            RefreshInstance(ref newPlayer, m_PlayerPrefab);

            m_PlayersDictionary.Add(newPlayerUserId, newPlayer);
        }
        
        private static void RefreshInstance(ref Player player, Player prefab)
        {
            var position = Vector3.zero;
            var rotation = Quaternion.identity;

            if (player != null)
            {
                var playerTransform = player.transform;
                position = playerTransform.position;
                rotation = playerTransform.rotation;
                PhotonNetwork.Destroy(player.gameObject);
            }

            player = PhotonNetwork.Instantiate("Prefabs/Entities/Units/Players/" + prefab.gameObject.name, position, rotation).GetComponent<Player>();
        }
    }
}