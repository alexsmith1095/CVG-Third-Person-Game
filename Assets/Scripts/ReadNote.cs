using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XboxCtrlrInput;

public class ReadNote : MonoBehaviour {

    public Image noteImage;

	void Start () {
		noteImage.enabled = false;
    }

	void Update() {
		if (Input.GetKeyDown(KeyCode.N) || XCI.GetDPadDown(XboxDPad.Up)) {
			noteImage.enabled = !noteImage.enabled;
        }

		if (GetComponent<Image>().enabled) {
			if (!noteImage.enabled && Input.GetKeyDown(KeyCode.Return) || XCI.GetButtonDown(XboxButton.B)) {
				noteImage.enabled = false;
			}
		}
	}
}
