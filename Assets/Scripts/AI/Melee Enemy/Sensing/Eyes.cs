using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.AI; 

public class Eyes : MonoBehaviour {

	//public member variables
	[HideInInspector]
	public float ViewAngleRad{
		get{
			return ViewAngle * Mathf.Deg2Rad; 
		}
	}

	//private member variables
	private Transform target; 
	private NavMeshAgent agent;
	public NavReferences agentReferences; 
	private Coroutine currentRoutine; 
	private bool playerInRange, playerSighted;  

	void Awake(){
		agent = agentReferences.GetComponent<NavMeshAgent> ();
	}

	//prefab customizable variables
	[SerializeField][Range(0,10)]
	public float ViewRange;
	[SerializeField][Range(0,360)]
	public float ViewAngle;
	[SerializeField][Range(1,100)]
	public float SightUpdateQuality;
	[SerializeField][Range(1,100)]
	public float SensingQuality;
	public LayerMask DetectionLayerMask; 
	[SerializeField][Range(0,1)]
	public float TargetOffsetDistance; 


	void Start(){
		
		StartCoroutine (SenseForPlayer ()); 
	}
		
	public Vector2 DirFromAngle(float angleInDegrees, bool isGlobal){
		if (!isGlobal) {
			angleInDegrees += transform.eulerAngles.z; 
		}
		return new Vector2 (Mathf.Sin(-angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(-angleInDegrees * Mathf.Deg2Rad));

	}

	//checa si el player esta dentro del rango
	IEnumerator SenseForPlayer(){
		ResumeSearch (); 
		while (true) {
			if (Physics2D.OverlapCircle (this.transform.position, ViewRange, DetectionLayerMask)) {
				playerInRange = true; 
			} else {
				playerInRange = false; 
				playerSighted = false; 
			}
			yield return new WaitForSeconds (2 / SensingQuality); 
		}
	}

	//checa si el player esta dentro del cono de vision
	IEnumerator LookForPlayer(){
		agentReferences.ResetReferenceTarget (); 
		while (true) {
			Debug.DrawLine (this.transform.position, agentReferences.Target2D.position, Color.white); 
			if (Physics2D.Raycast (this.transform.position, agentReferences.Target2D.position, ViewRange, DetectionLayerMask) 
				&& Vector2.Angle(this.transform.up, agentReferences.Target2D.position)<ViewAngle/2) {
				//pursuit target agent.SetDestination (agentReferences.Target.position - ((agentReferences.Target.position - this.transform.position).normalized * TargetOffsetDistance)); 
				playerSighted = true; 
			}
			yield return new WaitForSeconds (2 / SightUpdateQuality); 
		}
	}

	IEnumerator PursuitPlayer(){
		PauseSearch (); 

	}



	void ResumeSearch(){
		Debug.Log ("Resuming search");
		currentRoutine = null; 
		currentRoutine = StartCoroutine (LookForPlayer ()); 
	}

	void PauseSearch(){ 
		Debug.Log ("Pausing Search");
		if (currentRoutine != null) {
			StopCoroutine (currentRoutine);
		}
	}


}
