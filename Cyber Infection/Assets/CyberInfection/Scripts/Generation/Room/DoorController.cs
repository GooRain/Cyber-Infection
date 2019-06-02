using System;
using CyberInfection.Extension;
using CyberInfection.GameMechanics;
using CyberInfection.GameMechanics.Camera;
using CyberInfection.UI.Radar;
using UnityEngine;

namespace CyberInfection.Generation.Room
{
    public class DoorController : MonoBehaviour
    {
        public enum DoorType
        {
            Horizontal,
            Vertical
        }
        private DoorType _type;

        [SerializeField] private Door closedDoor;
        [SerializeField] private Door openedDoor;
        
        [SerializeField]
        private RoomController _left;
        [SerializeField]
        private RoomController _right;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
            
            openedDoor.onTriggerEnterEvent += OnOpenedDoorTriggerEnter;
            closedDoor.onCollisionEnterEvent += OnClosedDoorCollisionEnter;
        }

        private void OnOpenedDoorTriggerEnter(Collider2D other)
        {
            if (other.CompareTag(TagManager.PlayerTag))
            {
                var playerTransform = other.transform;

                MovePlayer(playerTransform);
            }
        }

        private void OnClosedDoorCollisionEnter(Collision2D other)
        {
            
        }

        public void Initialize(DoorType type, RoomController a, RoomController b)
        {
            _type = type;
            _left = a;
            _right = b;
            
            _left.AddDoor(this);
            _right.AddDoor(this);
        }

//        private void OnTriggerEnter2D(Collider2D other)
//        {
//            if (other.CompareTag(TagManager.PlayerTag))
//            {
//                var playerTransform = other.transform;
//
//                MovePlayer(playerTransform);
//            }
//        }

        private void MovePlayer(Transform playerTransform)
        {
            RoomController nextRoom;
            var position = _transform.position;
            position.z = playerTransform.position.z;
            int roomSide;
            switch(_type)
            {
                case DoorType.Horizontal:
                    var xLeft = playerTransform.position.x > position.x;
                    nextRoom = xLeft ? _left : _right;
                    playerTransform.position = position + Vector3.right * (xLeft ? -1 : 1);
                    roomSide = xLeft ? 1 : 2;
                    break;
                case DoorType.Vertical:
                    var yLeft = playerTransform.position.y > position.y;
                    nextRoom = yLeft ? _left : _right;
                    playerTransform.position = position + Vector3.up * (yLeft ? -1 : 1);
                    roomSide = yLeft ? 3 : 4;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            LevelController.instance.level.SelectRoomController(nextRoom);
            RadarController.instance.DrawRoom(roomSide);
        }

        public void Toggle(bool value)
        {
            Debug.Log(name + ".Toggle("+value+");");
            openedDoor.Toggle(value);
            closedDoor.Toggle(!value);
        }

        private void OnDestroy()
        {
            _left.RemoveDoor(this);
            _right.RemoveDoor(this);
        }
    }
}
