using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.GetComponent<Collider>().gameObject.tag == "Player") {
            PlayerEvents.PlayerDamaged(col.gameObject, 5);
		}
	}
}
