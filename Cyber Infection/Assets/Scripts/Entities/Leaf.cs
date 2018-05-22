using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf
{
	public Point pos;
	public Rectangle rect; // the position and size of this Leaf

	public Leaf leftChild; // the Leaf's left child Leaf
	public Leaf rightChild; // the Leaf's right child Leaf
	public Room room; // the room that is inside this Leaf
	public Hall halls; // hallways to connect this Leaf to other Leafs

	public Leaf(float _x, float _y, float _width, float _height)
	{
		pos = new Point
		{
			X = _x,
			Y = _y
		};
		rect = new Rectangle
		{
			width = _width,
			length = _height
		};
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
		if(rect.width > rect.length && rect.width / rect.length >= 1.25f)
			splitH = false;
		else if(rect.length > rect.width && rect.length / rect.width >= 1.25f)
			splitH = true;

		float max = (splitH ? rect.length : rect.width) - MapSettings.Ins.MinLeafSize; // determine the maximum height or width
		if(max <= MapSettings.Ins.MinLeafSize)
			return false; // the area is too small to split any more...

		float split = Random.Range(MapSettings.Ins.MinLeafSize, max); // determine where we're going to split

		// create our left and right children based on the direction of the split
		if(splitH)
		{
			leftChild = new Leaf(pos.X, pos.Y, rect.width, split);
			rightChild = new Leaf(pos.X, pos.Y + split, rect.width, rect.length - split);
		}
		else
		{
			leftChild = new Leaf(pos.X, pos.Y, split, rect.length);
			rightChild = new Leaf(pos.X + split, pos.Y, rect.width - split, rect.length);
		}
		return true; // split successful!
	}



}
