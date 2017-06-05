using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

[CustomEditor(typeof (PathFinding))]
public class FOV : Editor {
	private Proyection targetProyection;

	void OnSceneGUI(){
		PathFinding fov = (PathFinding)target; 
		Handles.color = Color.white; 
		Handles.DrawWireArc (fov.transform.position, Vector3.forward, Vector3.up, 360, fov.ViewRange); 
		Vector3 viewAngleA = fov.DirFromAngle (-fov.ViewAngle / 2, false); 
		Vector3 viewAngleB = fov.DirFromAngle (fov.ViewAngle / 2, false); 
		Handles.DrawLine (fov.transform.position, fov.transform.position + viewAngleA * fov.ViewRange);
		Handles.DrawLine (fov.transform.position, fov.transform.position + viewAngleB * fov.ViewRange); 

	}

}
