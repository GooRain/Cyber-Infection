using UnityEngine;

namespace UI
{
	public class MouseCursor : MonoBehaviour
	{
		[SerializeField] private Texture2D cursorTexture;
		private void Awake()
		{
			var hotSpot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
			Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
		}
	}
}