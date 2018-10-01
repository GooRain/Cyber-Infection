using UnityEngine;

namespace Scriptable
{
	[CreateAssetMenu(fileName = "Block Data", menuName = "Cyber Infection/Block Data", order = 1)]
	public class BlockData : ScriptableObject
	{
		public enum BlockType
		{
			Wall,
			Box
		}

		public BlockType type;
	}
}