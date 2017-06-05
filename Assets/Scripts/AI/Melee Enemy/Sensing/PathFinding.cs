using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.AI; 

public class PathFinding : MonoBehaviour {

	//public member variables
	[HideInInspector]
	public float ViewAngleRad{
		get{
			return ViewAngle * Mathf.Deg2Rad; 
		}
	}
	[HideInInspector]
	public NavReferences agentReferences; 

	//private member variables
	private Transform target; 
	private NavMeshAgent agent;
	private Coroutine courSearch, courPursuit, courReturn; 
	private bool playerInRange, playerSighted;  
 

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

	void Awake(){
		agent = agentReferences.GetComponent<NavMeshAgent> ();
	}

	void Start(){
		courSearch = courPursuit = null; 
		StartCoroutine (SenseForPlayer ());
		ResumeSearch (); 
	}
		
	public Vector2 DirFromAngle(float angleInDegrees, bool isGlobal){
		if (!isGlobal) {
			angleInDegrees += transform.eulerAngles.z; 
		}
		return new Vector2 (Mathf.Sin(-angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(-angleInDegrees * Mathf.Deg2Rad));

	}

	//checa si el player esta dentro del rango
	IEnumerator SenseForPlayer(){
		while (true) {
			if (Physics2D.OverlapCircle (this.transform.position, ViewRange, DetectionLayerMask)) {
				playerInRange = true; 
				Debug.Log ("Player in range"); 
			} else {
				playerInRange = false; 
				playerSighted = false;
			}
			if (playerSighted && courPursuit == null) {
				PauseSearch ();
				ResumePursuit ();
			} else if (!playerSighted && courPursuit != null) {
				Debug.Log ("Hey");
				PausePursuit (); 
				ResumeSearch (); 
			}
			yield return new WaitForSeconds (2 / SensingQuality); 
		}
	}

	//checa si el player esta dentro del cono de vision
	IEnumerator LookForPlayer(){
		while (true) {
			Debug.DrawLine (this.transform.position, agentReferences.Target2D.position, Color.white); 
			if (Physics2D.Raycast (this.transform.position, agentReferences.Target2D.position - this.transform.position, ViewRange, DetectionLayerMask)){
				Debug.Log (Vector2.Angle (this.transform.up, agentReferences.Target2D.position)); 
				if (Vector2.Angle (this.transform.up, agentReferences.Target2D.position) < ViewAngle / 2) {
					playerSighted = true; 
				}
			}
			yield return new WaitForSeconds (2 / SightUpdateQuality); 
		}
	}

	//asgina el destino del nav mesh 
	IEnumerator PursuitPlayer(){
		while (playerInRange) {
			agent.SetDestination (agentReferences.Target.position);
			yield return new WaitForSeconds (2 / SightUpdateQuality); 
		}
		yield return null; 
	}



	void ResumeSearch(){
		Debug.Log ("Resuming search");
		courSearch = null; 
		courSearch = StartCoroutine (LookForPlayer ()); 
	}

	void PauseSearch(){ 
		Debug.Log ("Pausing Search");
		if (courSearch != null) {
			StopCoroutine (courSearch);
			courSearch = null; 
		}
	}
	void ResumePursuit(){
		Debug.Log ("Resuming pursuit"); 
		courPursuit = null;
		courPursuit = StartCoroutine (PursuitPlayer ()); 
	}
	void PausePursuit(){
		Debug.Log ("Pausing pursuit"); 
		agent.SetDestination (agentReferences.OriginalPosition);
		if (courPursuit != null) {
			StopCoroutine (courPursuit);
			courPursuit = null;
		}
	}
		


}
