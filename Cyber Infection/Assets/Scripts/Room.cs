using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{

	public MeshRenderer meshRenderer;
	public RoomSettings Settings { get; set; }

	public void SetRandomColor()
	{
		meshRenderer.material.color = new Color(Random.Range(0.5f, 1f), Random.Range(0.5f, 1f), Random.Range(0.5f, 1f));
	}


}
