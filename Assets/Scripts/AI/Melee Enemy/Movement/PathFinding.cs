using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

	private Grid grid; 
	public Transform seeker, target; 

	void Wake(){
		grid = this.GetComponent<Grid> ();  

	}

	void LateUpdate(){
		FindPath (seeker.position, target.position); 
	}

	//a* 
	void FindPath(Vector3 startPos, Vector3 targetPos){
		Node startNode = grid.nodeFromWorldPoint (new Vector2(startPos.x, startPos.y));  
		Node targetNode = grid.nodeFromWorldPoint (new Vector2(targetPos.x, targetPos.y));

		List<Node> openSet = new List<Node> (); 
		HashSet<Node> closedSet = new HashSet<Node> (); 

		openSet.Add (startNode); 

		while (openSet.Count > 0) {
			Node currentNode = openSet [0]; 
			for (int i = 1; i < openSet.Count; i++) {
				if (openSet [i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost)) {
					currentNode = openSet [i]; 
				}
			}

			openSet.Remove (currentNode); 
			closedSet.Add (currentNode); 

			if (currentNode == targetNode) {
				retracePath (startNode, targetNode); 
				return; 
			} 

			foreach (Node neighbour in grid.getNeighbours(currentNode)) {
				if (!neighbour.walkable || closedSet.Contains (neighbour)) {
					continue; 
				} 
				int newMovementCostToNeighbour = currentNode.gCost + getDistanceBetweenNodes (currentNode, neighbour); 
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour; 
					neighbour.hCost = getDistanceBetweenNodes (neighbour, targetNode); 
					neighbour.parent = currentNode; 

					if (!openSet.Contains (neighbour)) {
						openSet.Add (neighbour); 
					}
				}
			}

		}
	
	}

	void retracePath(Node startNode, Node endNode){
		List<Node> path = new List<Node> (); 
		Node currentNode = endNode; 
		while (currentNode != startNode) {
			path.Add (currentNode); 
			currentNode = currentNode.parent; 
		}
		path.Reverse (); 
		grid.path = path; 
	}

	int getDistanceBetweenNodes(Node A, Node B){
		int distanceX = Mathf.Abs (A.gridPosX - B.gridPosX); 
		int distanceY = Mathf.Abs (A.gridPosY - B.gridPosY); 
		if (distanceX > distanceY) {
			return (14 * (distanceY) + 10 * (distanceX - distanceY)); 
		} else {
			return (14 * (distanceX) + 10 * (distanceY - distanceX)); 
		}
	}


}
