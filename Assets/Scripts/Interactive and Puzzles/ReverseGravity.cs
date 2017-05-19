using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseGravity : MonoBehaviour {

	void OnTriggerEnter (Collider col) {
		if (col.GetComponent<Collider>().gameObject.tag == "Player") {
			PlayerEvents.GravityToggled(); // Fire the toggle gravity event if collider is player
        }

		if (col.GetComponent<Collider>().gameObject.tag == "Pot") {
			StartCoroutine(PotGravity(col.transform));
		}
    }

	void OnTriggerExit (Collider col) {
		if (col.GetComponent<Collider>().gameObject.tag == "Player") {
            PlayerEvents.GravityToggled();
		}
	}

	IEnumerator PotGravity (Transform pot) {
		pot.GetComponent<ConstantForce>().force = new Vector3(0, 20.0f, 0);
		yield return new WaitForSeconds(3);
        pot.gameObject.SetActive(false);
		yield return null;
	}
}
