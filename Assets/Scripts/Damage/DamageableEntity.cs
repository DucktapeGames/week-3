using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour {

	public uint totalHp;
    public uint hp;
	Damageable dmg;

	private GameObject player2d; 

	public delegate void DeathEvents();
	public static event DeathEvents playerDied; 

	void Awake(){
		player2d = GameObject.FindGameObjectWithTag ("Player2D"); 
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
        }
    }

	IEnumerator Die() {
		playerDied (); 
        if(gameObject.tag == "Container") {
            Spawner spawner = GetComponent<Spawner>();
            spawner.Spawn();
        }
        //Destroy(gameObject);
		yield return new WaitForSeconds(1f); 
		player2d.SetActive (false); 
    }

	void DoDie(){
		StartCoroutine (Die ()); 
	}
}
