using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// Attach to child of pickup which should also have the trigger and text mesh attached

public class PickupNote : MonoBehaviour {

	void Awake() {
		GetComponent<MeshRenderer>().enabled = false; // Ensure the text is hidden on load
    }

	void Start () {
		if (XCI.GetNumPluggedCtrlrs() > 0)
            GetComponent<TextMesh>().text = "Press X to pick up";
        else
			GetComponent<TextMesh>().text = "Press E to pick up";
    }

	void FixedUpdate () {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
	}

	void OnTriggerStay(Collider col) {
		if (XCI.GetNumPluggedCtrlrs() > 0) {
			GetComponent<TextMesh>().text = "Press X to pick up";
		} else {
			GetComponent<TextMesh>().text = "Press E to pick up";
		}

		if(col.gameObject.tag == "Player") {
			GetComponent<MeshRenderer>().enabled = true;
			if(XCI.GetButtonDown(XboxButton.X)) {
                PlayerEvents.PickedUpObject(transform.parent.gameObject);
                PlayerEvents.DisplayPrompt("Press Up on D-Pad to read Note", 8);
				Destroy(transform.parent.gameObject);
	        } else if (Input.GetKeyDown(KeyCode.E)) {
                PlayerEvents.PickedUpObject(transform.parent.gameObject);
        		PlayerEvents.DisplayPrompt("Press N to read Note", 8);
				Destroy(transform.parent.gameObject);
			}
		}
    }

	void OnTriggerExit(Collider col) {
		if(col.gameObject.tag == "Player") {
            GetComponent<MeshRenderer>().enabled = false;
		}
    }
}
