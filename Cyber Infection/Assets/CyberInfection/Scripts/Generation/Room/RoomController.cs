using System;
using System.Collections.Generic;
using CyberInfection.GameMechanics.Entity.Units;
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
		private readonly List<Vector3Int> _floorTiles = new List<Vector3Int>();
		private readonly List<Vector3Int> _wallTiles = new List<Vector3Int>();
		private readonly List<Door> _myDoors = new List<Door>();

		private Transform _transform;

		public Room room { get; set; }

		private List<Enemy> _enemies;

		public List<Vector3Int> floorTiles => _floorTiles;
		public List<Vector3Int> wallTiles => _wallTiles;

		private void Awake()
		{
			_transform = transform;
		}

		public void AddDoor(Door door)
		{
			_myDoors.Add(door);
		}

		public void RemoveDoor(Door door)
		{
			_myDoors.Remove(door);
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

		private void Deactivate()
		{
			ColorAllTiles(Color.clear);

			ToggleDoors(true);
		}

		private void Activate()
		{
			ColorAllTiles(Color.white);

			if ((room.type & RoomType.Start) == 0)
			{
				ToggleDoors(false);
				SpawnEnemies();
			}
		}

		private void ColorAllTiles(Color color)
		{
			foreach (var pos in _floorTiles)
			{
				ColorTile(MapGenerator.instance.floorTilemap, pos, color);
			}

			foreach (var pos in _wallTiles)
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
				ToggleDoors(true);
			}
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
