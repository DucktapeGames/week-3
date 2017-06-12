using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour {

	public uint totalHp;
    public uint hp;
	Damageable dmg;
	private Rigidbody rbody; 

	public bool isPlayer; 
	private SpriteRenderer spriteRender; 
	private GameObject target2D; 
	public Sprite DeadSprite; 

	public delegate void DeathEvents();
	public static event DeathEvents playerDied; 

	void Awake(){
		rbody = this.GetComponent<Rigidbody> (); 
		if (isPlayer) {
			target2D = GameObject.FindGameObjectWithTag ("Player2D"); 
			spriteRender = target2D.GetComponent<SpriteRenderer> ();
		} else {
			spriteRender = this.GetComponent<Proyection> ().Target.gameObject.GetComponentInChildren<SpriteRenderer> (); 
			target2D = this.GetComponent<Proyection> ().Target.gameObject; 
		}

	}

    // Use this for initialization
    void Start () {
        dmg = GetComponent<Damageable>();
        dmg.Damage = Damage;
		hp = totalHp;
    }

    public void Damage(uint amount) {
        if (hp < amount){
            hp = 0;
            DoDie ();
        } 
		else {
            hp -= amount;
			rbody.AddExplosionForce (100, this.transform.position + this.transform.forward, 1f); 
        }
    }

	IEnumerator Die() {
        if(gameObject.tag == "Container") {
            //Spawner spawner = GetComponent<Spawner>();
            //spawner.Spawn();
        }
		if (isPlayer) {
			playerDied (); 
			target2D.GetComponent<Animator> ().enabled = false; 
			spriteRender.sprite = DeadSprite; 
			Destroy (this.gameObject); 
		} else {
			target2D.GetComponentInChildren<Animator> ().enabled = false; 
			target2D.GetComponent<Chasing> ().enabled = false; 
			target2D.GetComponent<PathFinding> ().enabled = false; 
			target2D.GetComponent<Chasing> ().enabled = false; 
			spriteRender.sprite = DeadSprite; 
			Destroy (this.gameObject); 
		}
		yield return new WaitForSeconds(1f); 


    }

	void DoDie(){
		StartCoroutine (Die ()); 
	}
}
