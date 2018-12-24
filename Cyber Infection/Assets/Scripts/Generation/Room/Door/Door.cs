using System;
using Extension;
using GameMechanic.CameraMechanic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Generation.Room.Door
{
    public class Door : MonoBehaviour
    {
        public enum DoorType
        {
            Horizontal,
            Vertical
        }
        private DoorType _type;

        [SerializeField]
        private RoomController _left;
        [SerializeField]
        private RoomController _right;
        public void Initialize(DoorType type, RoomController a, RoomController b)
        {
            _type = type;
            _left = a;
            _right = b;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(TagManager.PlayerTag))
            {
                var playerTransform = other.transform;

                MovePlayer(playerTransform.position);
            }
        }

        private void MovePlayer(Vector3 position)
        {
            RoomController nextRoom;
            switch(_type)
            {
                case DoorType.Horizontal:
                    nextRoom = position.x > transform.position.x ? _left : _right;
                    break;
                case DoorType.Vertical:
                    nextRoom = position.y > transform.position.y ? _left : _right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CameraController.instance.SetRoom(nextRoom);
        }
    }
}
