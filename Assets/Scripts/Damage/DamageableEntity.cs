using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageableEntity : MonoBehaviour {

	public uint totalHp;
    public uint hp;
	Damageable dmg;

    // Use this for initialization
    void Start () {
        dmg = GetComponent<Damageable>();
        dmg.Damage = Damage;
		hp = totalHp;
    }

    void Damage(uint amount) {
        if (hp < amount){
            hp = 0;
            Die ();
        } 
		else {
            hp -= amount;
        }
    }

    void Die() {
        if(gameObject.tag == "Container") {
            Spawner spawner = GetComponent<Spawner>();
            spawner.Spawn();
        }
        Destroy(gameObject);
    }
}
