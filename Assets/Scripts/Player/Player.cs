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

	void OnCollisionEnter(Collision col) {
		if(col.gameObject.tag == "Life") {
			if(dmg.hp + 20 >= dmg.totalHp) {
				dmg.hp = dmg.totalHp;
			}
			else {
				dmg.hp += 20;
			}
		}

		if(col.gameObject.tag == "Mana") {
			if(mana + 20 >= totalMana) {
				mana = totalMana;
			}
			else {
				mana += 20;
			}
		}

		if(col.gameObject.tag == "Stamina") {
			if(stamina + 20 >= totalStamina) {
				stamina = totalStamina;
			}
			else {
				stamina += 20;
			}
		}
		Debug.Log("yuss");
		Destroy(col.gameObject);
	}
}
