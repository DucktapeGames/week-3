using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	public delegate void OnDamage (uint damage);

    public OnDamage Damage = DamageDefault;

    static void DamageDefault(uint damage) {
        // Do nothing
    }
}
