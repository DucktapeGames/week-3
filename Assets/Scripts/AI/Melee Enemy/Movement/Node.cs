using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : IHeapItem<Node> {

	public bool walkable; 
	public Vector2 worldPosition; 
	public int gCost, hCost; 
	public int gridPosX, gridPosY; 
	public Node parent; 
	private int _heapIndex; 
	public int HeapIndex {
		get{
			return _heapIndex; 
		}
		set{
			_heapIndex = value; 
		}
	}
	public int fCost{
		get{
			return gCost + hCost; 
		}
	}

	public Node (bool _walkable, Vector2 _worldPosition, int _gridPosX, int _gridPosY){
		walkable = _walkable; 
		worldPosition = _worldPosition; 
		gridPosX = _gridPosX; 
		gridPosY = _gridPosY; 
	}

	public int CompareTo(Node nodeToCompare){
		int compareResult = fCost.CompareTo (nodeToCompare.fCost); 
		if (compareResult == 0) {
			compareResult = hCost.CompareTo (nodeToCompare.hCost); 
		}
		return -compareResult; 

	}

}
