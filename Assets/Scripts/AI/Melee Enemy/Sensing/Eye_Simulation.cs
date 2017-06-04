using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye_Simulation : MonoBehaviour {

	//customizable settings 
	[SerializeField][Range(0, 180)]
	public float viewAngle; 
	[SerializeField][Range(0, 1)]
	public float viewRange; 


	//references
	private CircleCollider2D range;
	public Transform body, playerTrans; 
	private RaycastHit2D hit; 
	private Coroutine curretRoutine; 
	private Pursuit pursuitScriptReferece; 
	private PathFinding pathfindingScriptReferece; 



	void Awake(){
		body = this.transform;
		playerTrans = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		pursuitScriptReferece = this.gameObject.GetComponent<Pursuit> (); 
		pathfindingScriptReferece = GameObject.FindGameObjectWithTag ("PathFindingManger").GetComponent<PathFinding> ();
	}
	  

	/*void OnDrawGizmos(){
		Gizmos.color = Color.white; 
		Gizmos.DrawWireSphere (body.position, viewRange * body.localScale.x); 
		Gizmos.color = Color.blue; 
		Gizmos.DrawLine (body.position,body.position + DirectionFromAngle (viewAngle - (body.rotation.z*100))*viewRange * body.localScale.x);
		Debug.Log (viewAngle + transform.rotation.z);
		Gizmos.color = Color.blue; 
		Gizmos.DrawLine (body.position,body.position + DirectionFromAngle (-viewAngle - (body.rotation.z*100))*viewRange * body.localScale.x);
	} */


	Vector3 DirectionFromAngle(float _angle){
		return new Vector3 (Mathf.Sin ((_angle + 180 ) * Mathf.Deg2Rad), Mathf.Cos ((_angle + 180 + transform.rotation.z) * Mathf.Deg2Rad), 0); 

	}


	/* Esta corutina se detiene sola cuando el enemigo vio al player
	 * solo cuando el player sale del rango (pierde al enemigo) y 
	 * vuelve a entrar a su rango el enemigo empieza de nuevo a 
	 * buscar al player (lanzar los raycasts) 
	 */
	public IEnumerator lookForPlayer(){
		while (true) {
			//Debug.Log (playerTrans.position);
			hit = Physics2D.Raycast (body.position, (playerTrans.position -body.position), viewRange * body.localScale.x ); 
			if (hit) {
				if (hit.transform.tag == "Player" && 
					Vector2.Angle(new Vector2(-transform.up.x, -transform.up.y), new Vector2(playerTrans.position.x, playerTrans.position.y))<viewAngle) {
					pursuitScriptReferece.resumePursuit (); 
					pathfindingScriptReferece.resumeGetPath (); 
					//Debug.Log ("Found him");
					break; 
				}
			}
			//Debug.DrawRay (body.position, (playerTrans.position -body.position).normalized *viewRange * body.localScale.x , Color.red); 

			yield return new WaitForSeconds (0.2f); 
		} 
	}


	public void resumeLookForPlayer(){
		//Debug.Log ("hey"); 
		curretRoutine = null; 
		curretRoutine = StartCoroutine (lookForPlayer ()); 
	}
	public void pauseLookForPlayer(){
		//Debug.Log ("owwww"); 
		if (curretRoutine != null) {
			StopCoroutine (curretRoutine); 
		}
	}

}
