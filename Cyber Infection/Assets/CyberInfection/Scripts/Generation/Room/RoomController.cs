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
		public Room room { get; set; }

		private List<Tile> _myTiles = new List<Tile>();

		private Transform _transform;

		private void Awake()
		{
			_transform = transform;
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
			var transparent = Color.white;
			transparent.a = 0f;
			foreach (var tile in _myTiles)
			{
				tile.color = transparent;
			}
		}

		public void Activate()
		{
			var transparent = Color.white;
			transparent.a = 1f;
			foreach (var tile in _myTiles)
			{
				tile.color = transparent;
			}
		}

		public Vector3 GetEnemySpawnPos()
		{
			var spawnPos = _transform.position;
			spawnPos.x += FiftyFifty() ? -5 : 5;
			return spawnPos;
		}

		private bool FiftyFifty()
		{
			return Random.Range(0, 2) == 0;
		}

		public void OnFocus()
		{
			if (!PhotonNetwork.IsMasterClient)
			{
				return;
			}
			
			var enemyCount = Random.Range(1, 3);
			for (var i = 0; i < enemyCount; i++)
			{
				Debug.Log("Enemy #" + i);
				EnemySpawner.instance.SpawnEnemy(GetEnemySpawnPos());
			}
		}

		public void OnUnFocus()
		{
			
		}
	}
}
