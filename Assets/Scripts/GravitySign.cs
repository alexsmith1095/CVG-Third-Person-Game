using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySign : MonoBehaviour {

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			PlayerEvents.DisplayPrompt("Falling upwards?", 100);
		}
	}

	void OnTriggerExit (Collider col) {
		if (col.tag == "Player") {
			PlayerEvents.DisplayPrompt("", 100);
		}
	}
}
