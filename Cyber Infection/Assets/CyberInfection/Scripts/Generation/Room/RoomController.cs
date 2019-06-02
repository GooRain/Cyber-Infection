using System;
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
		private readonly List<DoorController> _myDoors = new List<DoorController>();

		private Transform _transform;

		public Room room { get; set; }

		private List<Enemy> _enemies;

		public List<Vector3Int> FloorTiles { get; } = new List<Vector3Int>();
		public List<Vector3Int> WallTiles { get; } = new List<Vector3Int>();

		private void Awake()
		{
			_transform = transform;
		}

		public void AddDoor(DoorController doorController)
		{
			_myDoors.Add(doorController);
		}

		public void RemoveDoor(DoorController doorController)
		{
			_myDoors.Remove(doorController);
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

			if ((room.type & RoomType.Start) != 0)
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

		private Color currentColor;
		public bool isCompleted;

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
			foreach (var door in _myDoors)
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
			
			_enemies = new List<Enemy>();
			
			var enemyCount = Random.Range(1, 3);
			for (var i = 0; i < enemyCount; i++)
			{
				Debug.Log("Enemy #" + i);
				var enemy = EnemySpawner.instance.SpawnEnemy(GetEnemySpawnPos());
				_enemies.Add(enemy);
				enemy.OnDeath += OnEnemyDeath;
			}
		}

		private void OnEnemyDeath(Enemy enemy)
		{
			_enemies.Remove(enemy);

			CheckForRoomPass();
		}

		private void CheckForRoomPass()
		{
			if (IsRoomClear())
			{
				CompleteRoom();
			}
		}

		private void CompleteRoom()
		{
			LevelController.instance.RoomIsCompleted(this);
			isCompleted = true;
			ToggleDoors(true);
		}

		private bool IsRoomClear()
		{
			return _enemies.Count < 1;
		}

		private Vector3 GetEnemySpawnPos()
		{
			var spawnPos = _transform.position;
			spawnPos.x += FiftyFifty() ? -5 : 5;
			return spawnPos;
		}
	}
}
