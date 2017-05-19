using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

// Attach to child of pickup which should also have the trigger and text mesh attached

public class PickupNote : MonoBehaviour {

    public GameObject noteUI;
    public GameObject noteIcon;

	void Start () {
		GetComponent<MeshRenderer>().enabled = false; // Ensure the text is hidden on load
        noteUI.SetActive(false);
		noteIcon.SetActive(false);
    }

	void FixedUpdate () {
		// Text always faces the player camera
		transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0);
    }

	void OnTriggerEnter () {
		if (XCI.GetNumPluggedCtrlrs() > 0)
            GetComponent<TextMesh>().text = "Press Y to pick up";
        else
			GetComponent<TextMesh>().text = "Press E to pick up";
	}

	void OnTriggerStay(Collider col) {
		if(col.gameObject.tag == "Player") {
			GetComponent<MeshRenderer>().enabled = true;

			if (Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.Y)) {
                // PlayerEvents.PickedUpObject(transform.parent.gameObject);
				transform.parent.gameObject.SetActive(false);
                noteUI.SetActive(true);
				noteIcon.SetActive(true);

				if (XCI.GetNumPluggedCtrlrs() > 0) {
					PlayerEvents.DisplayPrompt("Press Up on D-Pad to read Note", 8);
				} else {
					PlayerEvents.DisplayPrompt("Press N to read Note", 8);
				}
			}
		}
    }

	void OnTriggerExit(Collider col) {
		if(col.gameObject.tag == "Player") {
            GetComponent<MeshRenderer>().enabled = false;
		}
    }
}
