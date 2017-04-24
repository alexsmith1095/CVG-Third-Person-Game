using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTile : MonoBehaviour {

    public bool breakable = false;
    new Rigidbody rigidbody;

	void Start() {
		rigidbody = GetComponent<Rigidbody>();
    }

	void Collapse() {
        rigidbody.isKinematic = false;
		rigidbody.AddForce(0, -20f, 0, ForceMode.Impulse);
    }

    void RecieveMessage (int id) {
        if(breakable)
            Collapse();
 }
}
