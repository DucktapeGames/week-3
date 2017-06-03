using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour {

	private Grid grid; 
	public Transform target; 
	private Coroutine currentRoutine; 

	void Awake(){
		target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform> ();
	}

	void Start(){
		grid = GameObject.FindGameObjectWithTag ("PathFindingManager").GetComponent<Grid> ();
		Eye_Simulation.foundPlayer += resumeGetPath; 
		Proximity_Sensing.lostPlayer += pauseGetPath; 
	} 

	IEnumerator getPath(){
		while(true){
			FindPath (this.transform.position, target.position); 
			yield return new WaitForSeconds(0.4f);
		}
	}
		
	public void FindPath(Vector3 startPos, Vector3 targetPos){
		Node startNode = grid.nodeFromWorldPoint (new Vector2(startPos.x, startPos.y));  
		Node targetNode = grid.nodeFromWorldPoint (new Vector2(targetPos.x, targetPos.y));

		Heap<Node> openSet = new Heap<Node> (grid.MaxSize); 
		HashSet<Node> closedSet = new HashSet<Node> (); 

		openSet.Add (startNode); 

		while (openSet.Count > 0) {
			Node currentNode = openSet.Pop();  
			closedSet.Add (currentNode); 

			if (currentNode == targetNode) {
				retracePath (startNode, targetNode); 
				return; 
			} 

			foreach (Node neighbour in grid.getNeighbours(currentNode)) {
				if (neighbour.walkable || closedSet.Contains (neighbour)) {
					continue; 
				} 
				int newMovementCostToNeighbour = currentNode.gCost + getDistanceBetweenNodes (currentNode, neighbour); 
				if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains (neighbour)) {
					neighbour.gCost = newMovementCostToNeighbour; 
					neighbour.hCost = getDistanceBetweenNodes (neighbour, targetNode); 
					neighbour.parent = currentNode; 

					if (!openSet.Contains (neighbour)) {
						openSet.Add (neighbour); 
						openSet.UpdateItem (neighbour);
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
		if (path != null) {
			grid.path = path;
		}
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


	void resumeGetPath(){
		currentRoutine = null; 
		currentRoutine = StartCoroutine (getPath ()); 

	}
	void pauseGetPath(){
		if (currentRoutine != null) {
			StopCoroutine (currentRoutine); 
		}
	}

}
