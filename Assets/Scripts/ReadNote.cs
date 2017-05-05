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
            if (noteImage.enabled && XCI.GetNumPluggedCtrlrs() > 0)
                PlayerEvents.DisplayPrompt("Press B to close", 100);
            else if (noteImage.enabled)
                PlayerEvents.DisplayPrompt("Press N to close", 100);
            else if (!noteImage.enabled)
                PlayerEvents.DisplayPrompt("", 100);
        }

		if (GetComponent<Image>().enabled) {
			if (!noteImage.enabled && Input.GetKeyDown(KeyCode.Return) || XCI.GetButtonDown(XboxButton.B)) {
                noteImage.enabled = false;
                PlayerEvents.DisplayPrompt("", 100);
			}
		}
	}
}
