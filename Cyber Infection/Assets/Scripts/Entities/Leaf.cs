using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
	public Point pos;
	public Rectangle rect; // the position and size of this Leaf

	public Leaf leftChild; // the Leaf's left child Leaf
	public Leaf rightChild; // the Leaf's right child Leaf
	public RoomSettings room; // the room that is inside this Leaf
	public List<Rectangle> halls; // hallways to connect this Leaf to other Leafs

	public Leaf(float _x, float _y, float _width, float _height)
	{
		pos = new Point(_x, _y);
		rect = new Rectangle(_width, _height);
	}

	public bool Split()
	{
		// begin splitting the leaf into two children
		if(leftChild != null || rightChild != null)
			return false; // we're already split! Abort!

		// determine direction of split
		// if the width is >25% larger than height, we split vertically
		// if the height is >25% larger than the width, we split horizontally
		// otherwise we split randomly
		bool splitH = Random.Range(0f, 1f) > 0.5f;
		if(rect.width > rect.height && rect.width / rect.height >= 1.25f)
			splitH = false;
		else if(rect.height > rect.width && rect.height / rect.width >= 1.25f)
			splitH = true;

		float max = (splitH ? rect.height : rect.width) - MapSettings.Ins.MinLeafSize; // determine the maximum height or width
		if(max <= MapSettings.Ins.MinLeafSize)
			return false; // the area is too small to split any more...

		float split = Random.Range(MapSettings.Ins.MinLeafSize, max); // determine where we're going to split

		// create our left and right children based on the direction of the split
		if(splitH)
		{
			leftChild = new Leaf(pos.X, pos.Y, rect.width, split);
			rightChild = new Leaf(pos.X, pos.Y + split, rect.width, rect.height - split);
		}
		else
		{
			leftChild = new Leaf(pos.X, pos.Y, split, rect.height);
			rightChild = new Leaf(pos.X + split, pos.Y, rect.width - split, rect.height);
		}
		return true; // split successful!
	}

	public void CreateRooms()
	{
		// this function generates all the rooms and hallways for this Leaf and all of its children.
		if(leftChild != null || rightChild != null)
		{
			// this leaf has been split, so go into the children leafs
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
			// this Leaf is the ready to make a room
			Rectangle roomSize;
			Point roomPos;
			float minSize = Mathf.Sqrt(MapSettings.Ins.MinLeafSize);
			// the room can be between 3 x 3 tiles to the size of the leaf - 2.
			roomSize = new Rectangle(Random.Range(minSize, rect.width), Random.Range(minSize, rect.height));
			// place the room within the Leaf, but don't put it right 
			// against the side of the Leaf (that would merge rooms together)
			roomPos = new Point(Random.Range(1f, rect.width - roomSize.width - 1f), Random.Range(1f, rect.height - roomSize.height - 1f));
			room = new RoomSettings(pos + roomPos, roomSize);
			MapGenerator.Ins.AddRoom(this);
		}

		//room = new RoomSettings(pos, rect, Random.Range(-2.5f, -7.5f));
		//MapGenerator.Ins.AddRoom(this);
	}
	
}
