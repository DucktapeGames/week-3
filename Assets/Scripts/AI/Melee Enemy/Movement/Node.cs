using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {

	public bool walkable; 
	public Vector2 worldPosition; 
	public int gCost, hCost; 
	public int gridPosX, gridPosY; 
	public Node parent; 
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

}
