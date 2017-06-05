using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyection : MonoBehaviour {

	public Transform Target;
	public Transform Floor; 
	public bool Static; 
	[SerializeField][Range(1,100)]
	public float Quality;
	private Coroutine currentRoutine; 

	void Start(){
		if (Target != null && Floor != null) {
			Target.position = new Vector3 (this.transform.position.x, this.transform.position.z - Floor.position.z, 0); 
			Target.eulerAngles = new Vector3(0,0, -this.transform.eulerAngles.y);
			if (!Static) {
				ResumeReflection (); 
			}
		}

	}

	IEnumerator Reflect(){
		while (true) {
			Target.position = new Vector3 (this.transform.position.x, this.transform.position.z - Floor.position.z, 0);
			Target.eulerAngles = new Vector3(0,0, -this.transform.eulerAngles.y);

			yield return new WaitForSeconds (2 / Quality); 
		}
	}

	void ResumeReflection(){
		currentRoutine = null; 
		currentRoutine = StartCoroutine (Reflect ()); 
	}
	void PauseReflection(){
		if (currentRoutine != null) {
			StopCoroutine (currentRoutine);
		}
	}



}
