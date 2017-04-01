using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTile : MonoBehaviour {

    Rigidbody rigidbody;

	void Start() {
		rigidbody = GetComponent<Rigidbody>();
    }

	void OnTriggerEnter(Collider col) {
		if(col.GetComponent<Collider>().gameObject.tag == "Player") {
			Collapse();
		}
	}

	void Collapse() {
        rigidbody.isKinematic = false;
		rigidbody.AddForce(0, -20f, 0, ForceMode.Impulse);
	}
}
