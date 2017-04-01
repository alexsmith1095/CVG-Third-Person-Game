using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenPrompt : MonoBehaviour {

	void Awake() {
		PlayerEvents.displayPrompt += SetPromptText;
	}

	void SetPromptText(string text) {
		GetComponent<Text>().text = text;
		StartCoroutine("ShowPrompt");
    }

	IEnumerator ShowPrompt() {
		GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(5);
        GetComponent<Text>().enabled = false;
	}
}
