using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTile : MonoBehaviour {

    public bool breakable = false;
	public AudioClip collapseSound;
	Rigidbody rigidbody;

	void Start() {
		rigidbody = GetComponent<Rigidbody>();
    }

	void Collapse() {
		SoundManager.Main.Play (collapseSound);
        rigidbody.isKinematic = false;
		rigidbody.AddForce(0, -20f, 0, ForceMode.Impulse);
    }

    void RecieveMessage (int id) {
        if(breakable)
            Collapse();
 }
}
