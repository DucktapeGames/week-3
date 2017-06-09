using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavReferences : MonoBehaviour {

	private Transform _target;

	[HideInInspector] 
	public Vector3 OriginalPosition;
	[HideInInspector]
	public Quaternion OriginalRotation; 

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


	void Awake(){
		OriginalPosition = this.transform.position; 
		OriginalRotation = this.transform.rotation; 
	}

	public void ResetReferenceTarget(){
		_target = null; 
	}


}
