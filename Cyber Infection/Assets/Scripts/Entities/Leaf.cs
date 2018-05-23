using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
	public Point pos;
	public Rectangle rect;

	public Leaf leftChild;
	public Leaf rightChild;
	public RoomSettings room;

	public Leaf(float _x, float _y, int _width, int _height)
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

		int max = (splitH ? rect.height : rect.width) - MapSettings.Ins.MinLeafSize;
		if(max <= MapSettings.Ins.MinLeafSize)
			return false;

		int split = Random.Range(MapSettings.Ins.MinLeafSize, max);

		if(splitH)
		{
			leftChild = new Leaf(pos.X, pos.Y - (rect.height - split) / 2f, rect.width, split);
			rightChild = new Leaf(pos.X, pos.Y + split / 2f, rect.width, rect.height - split);
		}
		else
		{
			leftChild = new Leaf(pos.X - (rect.width - split) / 2f, pos.Y, split, rect.height);
			rightChild = new Leaf(pos.X + split / 2f, pos.Y, rect.width - split, rect.height);
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
		{
			//Rectangle roomSize;
			//Point roomPos;
			//float minSize = Mathf.Sqrt(MapSettings.Ins.MinLeafSize);
			//roomSize = new Rectangle(Random.Range(minSize, rect.width), Random.Range(minSize, rect.height));
			//roomPos = new Point(Random.Range(1f, rect.width - roomSize.width - 1f), Random.Range(1f, rect.height - roomSize.height - 1f));
			room = new RoomSettings(pos, rect);
			MapGenerator.Ins.AddRoom(this);
		}

		//room = new RoomSettings(pos, rect, Random.Range(-2.5f, -7.5f));
		//MapGenerator.Ins.AddRoom(this);
	}

}
