using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour {

	void OnTriggerEnter(Collider col) {
		if(col.GetComponent<Collider>().gameObject.tag == "Player") {
            PlayerEvents.GravityToggled(); // Fire the toggle gravity event if collider is player
		}
    }

	void OnTriggerExit(Collider col) {
		if(col.GetComponent<Collider>().gameObject.tag == "Player") {
            PlayerEvents.GravityToggled();
		}
	}
}
