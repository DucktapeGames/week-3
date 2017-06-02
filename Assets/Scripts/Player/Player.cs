using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	public uint totalMana;
	uint mana;
	public uint totalStamina;
	uint stamina;
	DamageableEntity dmg;
	// Use this for initialization
	void Start () {
		dmg = GetComponent<DamageableEntity>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.tag == "Life") {
			if(dmg.hp + 20 >= dmg.totalHp) {
				dmg.hp = dmg.totalHp;
			}
			else {
				dmg.hp += 20;
			}
		}

		if(other.tag == "Mana") {
			if(mana + 20 >= totalMana) {
				mana = totalMana;
			}
			else {
				mana += 20;
			}
		}

		if(other.tag == "Stamina") {
			if(stamina + 20 >= totalStamina) {
				stamina = totalStamina;
			}
			else {
				stamina += 20;
			}
		}
		Debug.Log("yuss");
		//Destroy(col.gameObject);
	}
}
