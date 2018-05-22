using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	[Header("References")]
	public Map map;
	[Header("Prefabs")]
	public Room roomPrefab;

	private Leaf root;
	private List<Leaf> leafs;
	private IEnumerator generationCoroutine;

	private void Start()
	{
		generationCoroutine = Generate();
		StartCoroutine(generationCoroutine);
	}

	private IEnumerator Generate()
	{
		root = new Leaf(transform.position.x, transform.position.y, MapSettings.Ins.mapSize.width, MapSettings.Ins.mapSize.length);
		leafs = new List<Leaf>();

		Debug.Log("Leafs initialization Is Done!");

		generationCoroutine = Split();
		yield return StartCoroutine(generationCoroutine);

		Debug.Log("Leafs splitting Is Done!");

		generationCoroutine = PlaceRooms();
		yield return StartCoroutine(generationCoroutine);

		Debug.Log("Rooms placing Is Done!");
	}

	private IEnumerator Split()
	{
		leafs.Add(root);

		bool did_split = true;
		// we loop through every Leaf in our Vector over and over again, until no more Leafs can be split.
		while(did_split)
		{
			did_split = false;
			foreach(Leaf l in leafs)
			{
				if(l.leftChild == null && l.rightChild == null) // if this Leaf is not already split...
				{
					// if this Leaf is too big, or 75% chance...
					if(l.rect.width > MapSettings.Ins.MaxLeafSize || l.rect.length > MapSettings.Ins.MaxLeafSize || Random.Range(0f, 1f) > 0.25)
					{
						if(l.Split()) // split the Leaf!
						{
							// if we did split, push the child leafs to the Vector so we can loop into them next
							leafs.Add(l.leftChild);
							leafs.Add(l.rightChild);
							did_split = true;
							break;
						}
					}
				}
			}

			yield return new WaitForEndOfFrame();
		}

		yield return true;
	}

	private IEnumerator PlaceRooms()
	{
		foreach(var item in leafs)
		{
			Room _room = Instantiate(roomPrefab, item.pos.GetVector3(transform.position.z), Quaternion.identity, map.transform);
			_room.transform.localScale = item.rect.GetVector3();
			_room.SetRandomColor();
			map.rooms.Add(_room);
		}
		yield return true;
	}

}
