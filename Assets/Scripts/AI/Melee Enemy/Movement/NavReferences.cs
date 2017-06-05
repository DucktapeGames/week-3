using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavReferences : MonoBehaviour {

	private Transform _target;
	private Transform _target2D; 

	[HideInInspector] 
	public Vector3 OriginalPosition; 

	public  Transform Target{
		get{
			if (_target == null) {
				_target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();  
				return _target; 
			}
			return _target; 
		}
		set{
			_target = value; 
		}
	}
	public Transform Target2D{
		get{
			if (_target2D == null) {
				_target2D = GameObject.FindGameObjectWithTag ("Player2D").GetComponent<Transform>();  
				return _target2D; 
			}
			return _target2D; 
		}
		set{
			_target2D = value; 
		}

	}

	[HideInInspector]
	public bool FoundTarget; 


	void Awake(){
		FoundTarget = false; 
		this.gameObject.GetComponent<Proyection> ().Target.GetComponent<PathFinding> ().agentReferences = this; 
		OriginalPosition = this.transform.position; 
		_target2D = GameObject.FindGameObjectWithTag ("Player2D").GetComponent<Transform>();  


	}

	public void ResetReferenceTarget(){
		_target = null; 
	}


}
