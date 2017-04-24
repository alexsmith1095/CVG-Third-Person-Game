using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class ReadNote : MonoBehaviour {

	void Awake() {
        PlayerEvents.pickedUpObject += AddNoteToInventory;
		GetComponent<Image>().enabled = false;
    }

	void AddNoteToInventory(GameObject pickup) {
		// Find a UI version of the sprite picked up
		GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + pickup.name + "UI");
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.N) || XCI.GetDPadDown(XboxDPad.Up)) {
			if(GetComponent<Image>().sprite != null)
				GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
        }

		if (XCI.GetNumPluggedCtrlrs() > 0) {
			if (GetComponent<Image>().enabled && XCI.GetButtonDown(XboxButton.B)) {
				GetComponent<Image>().enabled = false;
			}
        } else {
            if (Input.GetKeyDown(KeyCode.Return))
				GetComponent<Image>().enabled = false;
		}
	}
}
