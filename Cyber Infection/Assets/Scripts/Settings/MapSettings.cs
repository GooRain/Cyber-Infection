using UnityEngine;

public class MapSettings : MonoBehaviour
{

	//_______________SINGLETON________________
	public static MapSettings ins;

	private void Awake()
	{
		if(ins != null)
			Destroy(gameObject);
		else
		{
			ins = this;
		}
	}
	//________________________________________

	public BlocksAsset[] blocksAssets;

	[Header("Map Settings")]
	public int maxRoomsCount = 5;

	[Header("Block Settings")]
	public Vector2Int blockSize;

	[Header("Room Settings")]
	public int roomWidth;
	public int roomHeight;

	private int currentRoomsCount = 0;

	public void RoomSpawned()
	{
		currentRoomsCount++;
	}

	public bool CanSpawnRoom()
	{
		if(currentRoomsCount < maxRoomsCount)
			return true;
		return false;
	}

	public BlocksAsset FindBlocks(BlockType type)
	{
		foreach(var item in blocksAssets)
		{
			if(item.type.Equals(type))
				return item;
		}
		Debug.LogError("No blocks assets of type: " + type + " has been assigned!");
		return null;
	}

}
