using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid: MonoBehaviour {

	public Transform player; 
	public LayerMask unWalkableMask; 
	public Vector2 gridSize; //the real float values
	public float nodeRadius; 
	public Node[,] grid; 
	public Transform mapCenter;
	float nodeDiameter; 
	int gridSizeX, gridSizeY; //how many nodes on the x and y; 
	public List<Node> path; 

	void Start(){
		nodeDiameter = nodeRadius * 2; 
		gridSizeX =Mathf.RoundToInt (gridSize.x / nodeDiameter); 
		gridSizeY =Mathf.RoundToInt (gridSize.y / nodeDiameter); 
		createGrid (); 
		Debug.Log (gridSizeX + " " + gridSizeY); 
	}

	void createGrid(){
		grid = new Node[gridSizeX, gridSizeY]; 
		Vector2 worldBottonLeft = new Vector2(mapCenter.position.x, mapCenter.position.y) - (Vector2.right * (gridSize.x / 2)) - (Vector2.up * (gridSize.y / 2));
		for (int x = 0; x < gridSizeX; x++) {
			for (int y = 0; y < gridSizeY; y++) {
				Vector2 worldPoint = worldBottonLeft + (Vector2.right * (x * nodeDiameter + nodeRadius)) + (Vector2.up * (y * nodeDiameter + nodeRadius)); 
				bool walkable = Physics2D.OverlapCircle(worldPoint, nodeRadius, unWalkableMask); 

				grid [x, y] = new Node (walkable, worldPoint, x, y); 
			}
		}
	}
		
	public Node nodeFromWorldPoint(Vector2 _worldpos){
		float percentx = (_worldpos.x + (gridSize.x / 2)) / gridSize.x; 
		float percenty = (_worldpos.y + (gridSize.y / 2)) / gridSize.y; 
		percentx = Mathf.Clamp01 (percentx);
		percenty = Mathf.Clamp01 (percenty); 
		int x = Mathf.RoundToInt((gridSizeX - 1) * percentx); 
		int y = Mathf.RoundToInt((gridSizeY - 1) * percenty); 
		Debug.Log (x + " " + y); 
		return grid [x, y]; 
	}

	public List<Node> getNeighbours(Node _node){
			List<Node> neighbours = new List<Node> (); 
			for (int x = -1; x <= 1; x++) {
				for (int y = -1; y <= 1; y++) {
					if (x == 0 && y == 0) {
						continue; 
					} 
					int checkX = _node.gridPosX + x;
					int checkY = _node.gridPosY + y;

					if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY) {
						neighbours.Add (grid [checkX, checkY]);  
					}

				}
			}
			return neighbours; 
		}

	void OnDrawGizmos(){
		Gizmos.color = Color.black; 
		Gizmos.DrawWireCube (mapCenter.position, new Vector3(gridSize.x, gridSize.y, 0));
		if (grid != null) {
			Node playerNode = nodeFromWorldPoint(new Vector2(player.position.x, player.position.y)); 
			foreach (Node n in grid) {
				Gizmos.color = (n.walkable)?Color.red:Color.white; 
				if (playerNode == n) {
					Gizmos.color = Color.green;
				}
				if(path!=null){
					if (path.Contains (n)) {
						Gizmos.color = Color.blue; 
					}
				}
				
				Gizmos.DrawCube (new Vector3(n.worldPosition.x,n.worldPosition.y,0.4f), Vector3.one * (nodeDiameter-0.1f)); 
			}
		}
	}



}
