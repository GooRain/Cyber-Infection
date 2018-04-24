using UnityEngine;

[System.Serializable]
public class BlocksAsset : ScriptableObject
{

	public BlockType type;
	public Block[] blockPrefab;

	public Block RandomBlock()
	{
		if(blockPrefab.Length > 0)
		{
			int index = Random.Range(0, blockPrefab.Length);
			return blockPrefab[index];
		}
		else
		{
			Debug.LogWarning("This asset doesn't have any block prefabs!");
			return null;
		}
	}

}
