using System.Collections.Generic;
using Extension;
using Generation.Tiles;
using UnityEngine;

namespace Generation.Room
{
	public class Room : MonoBehaviour
	{

		public MeshRenderer meshRenderer;
		public RoomSettings Settings { get; set; }
		public List<MapWall> Walls { get; set; }

		private void Awake()
		{
			Walls = new List<MapWall>();
		}

		public void SetRandomColor()
		{
			meshRenderer.material.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
		}


	}
}
