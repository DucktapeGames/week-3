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
	public bool moving, attacking;  

	public NavReferences agentReferences; 

	//private member variables
	private NavMeshAgent agent;
	private bool playerInRange, playerIsNotDead; 
	private RaycastHit2D hit; 
	private Transform player;
	private Coroutine Sensing, Looking; 

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
	public uint Damage; 

	void Awake(){
		agent = agentReferences.gameObject.GetComponent<NavMeshAgent> (); 
		player = GameObject.FindGameObjectWithTag ("Player2D").transform; 
		playerIsNotDead = true; 
	}

	void Start(){
		Sensing = StartCoroutine (SenseForPlayer ());
		Looking = StartCoroutine (LookForPlayer ()); 
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
		while (playerIsNotDead) {
			if (Physics2D.OverlapCircle (this.transform.position, ViewRange, DetectionLayerMask)) {
				playerInRange = true; 
			} else {
				playerInRange = false; 
			}
			yield return new WaitForSeconds (2 / SensingQuality); 
		}
	}

	//checa si el player esta dentro del cono de vision
	IEnumerator LookForPlayer(){
		while (playerIsNotDead && agentReferences != null) {
			if (Physics2D.Raycast (this.transform.position, (player.position - this.transform.position), ViewRange)){
				hit = Physics2D.Raycast (this.transform.position, (player.position - this.transform.position), ViewRange);
				//Debug.Log (hit.collider.tag + hit.collider.gameObject.name );
				Debug.DrawLine (this.transform.position, new Vector3 (hit.point.x, hit.point.y, 0), Color.white); 
				if (Vector3.Angle (this.transform.up, (player.position- this.transform.position))<(ViewAngle/2) && hit.collider.tag == "Player2D") {
					moving = true; 
					yield return StartCoroutine(PursuitPlayer()); 
				}
				//Debug.Log (Vector2.Angle (this.transform.up, (agentReferences.Target2D.position - this.transform.position)));
			}
			if (agent.remainingDistance == 0f) {
				agent.transform.rotation = Quaternion.Lerp (agent.transform.rotation, agentReferences.OriginalRotation, 10 * Time.fixedDeltaTime);
				moving = false; 
			}

			yield return new WaitForSeconds (2 / SightUpdateQuality); 
		}
	}

	//asgina el destino del nav mesh 
	IEnumerator PursuitPlayer(){
		if (agentReferences != null) {
			agent.stoppingDistance = 1.5f; 
		}
		while (playerInRange && playerIsNotDead && agentReferences != null) {
			agent.SetDestination (agentReferences.Target.position);
			if (agent.remainingDistance < 1f && Vector3.Angle (this.transform.up, (player.position - this.transform.position)) < (ViewAngle / 2)) {
				//Debug.Log ("Attemping to damage player");
				if (agentReferences.Target.gameObject.GetComponent<DamageableEntity> ()) {
					agentReferences.Target.gameObject.GetComponent<DamageableEntity> ().Damage (Damage);
					attacking = true; 
				}
			} else {
				attacking = false; 
			}
			yield return new WaitForSeconds (2 / SightUpdateQuality); 
		}
		if (agentReferences != null) {
			agent.SetDestination (agentReferences.OriginalPosition);
			agent.stoppingDistance = 0; 
		}
		yield return null; 
	}
		
	void ProtocolForWhenPlayerDied(){
		playerIsNotDead = false; 
		attacking = false; 
		moving = true; 

	}

	void OnDisable(){
		StopCoroutine (Sensing); 
		StopCoroutine (Looking); 
	}
		


}
