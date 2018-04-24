using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

	public List<Room> rooms;

	private void Awake()
	{
		rooms = new List<Room>();
	}

}
