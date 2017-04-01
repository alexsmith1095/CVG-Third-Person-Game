using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// Attach to child of pickup which should also have the trigger and text mesh attached

public class Pickup : MonoBehaviour {

	void Awake() {
		GetComponent<MeshRenderer>().enabled = false; // Ensure the text is hidden on load
	}

	void OnTriggerStay(Collider col) {
		if(col.gameObject.tag == "Player") {
			GetComponent<MeshRenderer>().enabled = true;
			if(XCI.GetButtonDown(XboxButton.X) || Input.GetKeyDown(KeyCode.E)) {
				Destroy(transform.parent.gameObject);
                PlayerEvents.PickedUpObject(transform.parent.gameObject);
				PlayerEvents.DisplayPrompt("Press N to read Note");
	        }
		}
    }

	void OnTriggerExit(Collider col) {
		if(col.gameObject.tag == "Player") {
            GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
