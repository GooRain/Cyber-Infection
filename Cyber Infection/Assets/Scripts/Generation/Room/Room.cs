using System.Collections.Generic;
using Extension;
using Generation.Tile;
using UnityEngine;

namespace Generation.Room
{
	public class Room : MonoBehaviour
	{

		public MeshRenderer meshRenderer;
		public RoomSettings Settings { get; set; }
		public List<Wall> Walls { get; set; }

		private void Awake()
		{
			Walls = new List<Wall>();
		}

		public void SetRandomColor()
		{
			meshRenderer.material.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
		}


	}
}
