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
	private NavMeshAgent agent;
	private Coroutine courSearch, courPursuit, courReturn; 
	private bool playerInRange, playerSighted, playerIsNotDead; 
 

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
	public LayerMask BlockDetectionLayer; 
	public uint Damage; 

	void Awake(){
		agent = agentReferences.GetComponent<NavMeshAgent> ();
		playerIsNotDead = true; 
	}

	void Start(){
		courSearch = courPursuit = null; 
		StartCoroutine (SenseForPlayer ());
		ResumeSearch (); 
		DamageableEntity.playerDied += ProtocolForWhenPlayerDied; 
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
			if (playerIsNotDead == false) {
				break; 
			}
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
			} else if ((!playerSighted && courPursuit != null)) {
				PausePursuit (); 
				ResumeSearch (); 
			}
			yield return new WaitForSeconds (2 / SensingQuality); 
		}
	}

	//checa si el player esta dentro del cono de vision
	IEnumerator LookForPlayer(){
		while (true) {
			if (playerIsNotDead == false) {
				break; 
			}
			//Debug.DrawLine (this.transform.position, (agentReferences.Target2D.position - this.transform.position).normalized, Color.white); 
			if (Physics2D.Raycast (this.transform.position, (agentReferences.Target2D.position - this.transform.position), ViewRange, DetectionLayerMask)
				&& !Physics2D.Raycast (this.transform.position, (agentReferences.Target2D.position - this.transform.position), ViewRange, BlockDetectionLayer)){
				//Debug.Log (Vector2.Angle (this.transform.up, (agentReferences.Target2D.position- this.transform.position).normalized)<(ViewAngle/2)); 
				//Debug.Log("Hey");
				if (Vector2.Angle (this.transform.up, (agentReferences.Target2D.position- this.transform.position))<(ViewAngle/2) && Vector2.Distance(this.transform.position, agentReferences.Target2D.position)<ViewRange*2) {
					playerSighted = true; 
				}
				//Debug.Log (Vector2.Angle (this.transform.up, (agentReferences.Target2D.position - this.transform.position)));
			}
			if (agent.remainingDistance == 0f) {
				agent.transform.rotation = Quaternion.Lerp (agent.transform.rotation, agentReferences.OriginalRotation, 10 * Time.fixedDeltaTime); 
			}
			yield return new WaitForSeconds (2 / SightUpdateQuality); 
		}
	}

	//asgina el destino del nav mesh 
	IEnumerator PursuitPlayer(){
		while (playerInRange) {
			if (playerIsNotDead == false) {
				break; 
			}
			if (agent.remainingDistance < 2.5f) {
				//Debug.Log ("Attemping to damage player");
				if (agentReferences.Target.gameObject.GetComponent<DamageableEntity> ()) {
					agentReferences.Target.gameObject.GetComponent<DamageableEntity> ().Damage (Damage);
					//Debug.Log ("Damage Success");
				}
			}
			agent.SetDestination (agentReferences.Target.position);
			yield return new WaitForSeconds (2 / SightUpdateQuality); 
		}
		yield return null; 
	}



	void ResumeSearch(){
		//Debug.Log ("Resuming search");
		courSearch = null; 
		courSearch = StartCoroutine (LookForPlayer ()); 
	}

	void PauseSearch(){ 
		//Debug.Log ("Pausing Search");
		if (courSearch != null) {
			StopCoroutine (courSearch);
			courSearch = null; 
		}
	}
	void ResumePursuit(){
		//Debug.Log ("Resuming pursuit"); 
		agent.stoppingDistance = 2.5f; 
		courPursuit = null;
		courPursuit = StartCoroutine (PursuitPlayer ()); 
	}
	void PausePursuit(){
		//Debug.Log ("Pausing pursuit"); 
		agent.SetDestination (agentReferences.OriginalPosition);
		agent.stoppingDistance = 0; 
		if (courPursuit != null) {
			StopCoroutine (courPursuit);
			courPursuit = null;
		}
	}


	void ProtocolForWhenPlayerDied(){
		playerIsNotDead = false; 
		PausePursuit (); 
	}
		


}
