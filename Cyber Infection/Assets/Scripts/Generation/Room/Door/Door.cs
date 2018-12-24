﻿using System;
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

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

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

                MovePlayer(playerTransform);
            }
        }

        private void MovePlayer(Transform playerTransform)
        {
            RoomController nextRoom;
            var position = _transform.position;
            switch(_type)
            {
                case DoorType.Horizontal:
                    var xLeft = playerTransform.position.x > position.x;
                    nextRoom = xLeft ? _left : _right;
                    playerTransform.position = position + Vector3.right * (xLeft ? -1 : 1);
                    break;
                case DoorType.Vertical:
                    var yLeft = playerTransform.position.y > position.y;
                    nextRoom = yLeft ? _left : _right;
                    playerTransform.position = position + Vector3.up * (yLeft ? -1 : 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            CameraController.instance.SetRoom(nextRoom);
        }
    }
}
