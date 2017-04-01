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
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + pickup.name + "UI");
	}

	void Update() {
		if(Input.GetKeyDown(KeyCode.N) || XCI.GetButtonDown(XboxButton.X)) {
			if(GetComponent<Image>().sprite != null)
				GetComponent<Image>().enabled = !GetComponent<Image>().enabled;
		}
	}
}
