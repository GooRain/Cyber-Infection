﻿using System;
using System.Collections;
using System.Collections.Generic;
using CyberInfection.GameMechanics;
using CyberInfection.GameMechanics.Entity.Units;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Photon.Pun;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace CyberInfection.Generation.Room
{
	[Flags]
	public enum Doors
	{
		None = 0,
		Right = 1,
		Up = 2,
		Left = 4,
		Down = 8,
		
		All = 15
	}
	public class RoomController : MonoBehaviour
	{
		private readonly List<DoorController> myDoors = new List<DoorController>();

		private Transform cachedTransform;

		private Color currentColor;

		private List<Enemy> enemies;
		
		public bool isCompleted;

		public RoomEntity RoomEntity { get; set; }
		public MapController MapController { get; set; }
		public List<Vector3Int> FloorTiles { get; } = new List<Vector3Int>();
		public List<Vector3Int> WallTiles { get; } = new List<Vector3Int>();
		public int RoomIndex { get; set; }

		private void Awake()
		{
			cachedTransform = transform;
		}

		public void AddDoor(DoorController doorController)
		{
			myDoors.Add(doorController);
		}

		public void RemoveDoor(DoorController doorController)
		{
			myDoors.Remove(doorController);
		}

		public void TryToToggle(bool value)
		{
			if (value)
			{
				// ...
				Activate();
				// ...
			}
			else
			{
				// ...
				Deactivate();
				// ...
			}
		}

		private TweenerCore<Color, Color, ColorOptions> coloringTween;
		
		private void Deactivate()
		{
			StartAnimateRoom(Color.clear);

			ToggleDoors(true);
		}

		private void Activate()
		{
			StartAnimateRoom(Color.white);

			if (isCompleted)
			{
				ToggleDoors(true);
				return;
			}

			if ((RoomEntity.Type & RoomType.Start) != 0)
			{
				ToggleDoors(true);
				return;
			}
			
			ToggleDoors(false);
			SpawnEnemies();
		}

		private void StartAnimateRoom(Color endValue)
		{
			coloringTween.Pause();
			coloringTween = DOTween.To(GetCurrentColor, ColorAllTiles, endValue, 2f);
			coloringTween.Play();
		}

		private Color GetCurrentColor()
		{
			return currentColor;
		}

		private void ColorAllTiles(Color color)
		{
			currentColor = color;
			
			foreach (var pos in FloorTiles)
			{
				ColorTile(MapGenerator.instance.FloorTilemap, pos, color);
			}

			foreach (var pos in WallTiles)
			{
				ColorTile(MapGenerator.instance.WallTilemap, pos, color);
			}
		}
		
		private void ColorTile(Tilemap tilemap, Vector3Int pos, Color color)
		{
			tilemap.SetTileFlags(pos, TileFlags.LockTransform);
			tilemap.SetColor(pos, color);
		}

		private void ToggleDoors(bool value)
		{
			foreach (var door in myDoors)
			{
				door.Toggle(value);
			}
		}

		private bool FiftyFifty()
		{
			return Random.Range(0, 2) == 0;
		}

		public void OnFocus()
		{
			
		}

		public void OnUnFocus()
		{
			
		}

		private void SpawnEnemies()
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				return;
			}
			
			enemies = new List<Enemy>();
			
			var enemyCount = Random.Range(1, 3);
			for (var i = 0; i < enemyCount; i++)
			{
				//Debug.Log("Enemy #" + i);
				var enemy = EnemySpawner.instance.SpawnEnemy(GetEnemySpawnPos());
				enemies.Add(enemy);
				enemy.OnDeath += OnEnemyDeath;
			}
		}

		private void OnEnemyDeath(Enemy enemy)
		{
			enemies.Remove(enemy);

			CheckForRoomPass();
		}

		private void CheckForRoomPass()
		{
			if (IsRoomClear())
			{
				MapController.RPC_CompleteRoom(RoomIndex);
			}
		}

		public void CompleteRoom()
		{
			isCompleted = true;
			
			LevelController.instance.RoomIsCompleted(this);
			ToggleDoors(true);
		}

		private bool IsRoomClear()
		{
			return enemies.Count < 1;
		}

		private Vector3 GetEnemySpawnPos()
		{
			var spawnPos = cachedTransform.position;
			spawnPos.x += FiftyFifty() ? -5 : 5;
			return spawnPos;
		}
	}
}
