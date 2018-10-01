using Generators;
using Persistent.Settings;
using UnityEngine;

namespace Entities
{
	public class Area
	{
		public Point pos;
		public Rectangle rect;

		public Area leftChild;
		public Area rightChild;
		public RoomSettings room;

		public Area(float _x, float _y, int _width, int _height)
		{
			pos = new Point(_x, _y);
			rect = new Rectangle(_width, _height);
		}

		public bool Split()
		{
			if(leftChild != null || rightChild != null)
				return false;

			bool splitH = Random.Range(0f, 1f) > 0.5f;
			if(rect.width > rect.height && (float)rect.width / rect.height >= 1.25f)
				splitH = false;
			else if(rect.height > rect.width && (float)rect.height / rect.width >= 1.25f)
				splitH = true;

			int max = (splitH ? rect.height : rect.width) - MapSettings.instance.MinAreaSize;
			if(max <= MapSettings.instance.MinAreaSize)
				return false;

			int split = Random.Range(MapSettings.instance.MinAreaSize, max);

			if(splitH)
			{
				leftChild = new Area(pos.X, pos.Y - (rect.height - split) / 2f, rect.width, split);
				rightChild = new Area(pos.X, pos.Y + split / 2f, rect.width, rect.height - split);
			}
			else
			{
				leftChild = new Area(pos.X - (rect.width - split) / 2f, pos.Y, split, rect.height);
				rightChild = new Area(pos.X + split / 2f, pos.Y, rect.width - split, rect.height);
			}
			return true;
		}

		public void CreateRooms()
		{
			if(leftChild != null || rightChild != null)
			{
				if(leftChild != null)
				{
					leftChild.CreateRooms();
				}
				if(rightChild != null)
				{
					rightChild.CreateRooms();
				}
			}
			else
			{room = new RoomSettings(pos, rect);
				MapGenerator.Ins.AddRoom(this);
			}
		}
	}
}
