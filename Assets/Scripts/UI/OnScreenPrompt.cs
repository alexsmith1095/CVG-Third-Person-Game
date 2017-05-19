using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnScreenPrompt : MonoBehaviour {

	void OnEnable () {
        PlayerEvents.displayPrompt += SetPromptText;
    }

    void OnDisable () {
        PlayerEvents.displayPrompt -= SetPromptText;
    }

	void SetPromptText(string text, int duration) {
		GetComponent<Text>().text = text;
		StartCoroutine(ShowPrompt(text, duration));
    }

	IEnumerator ShowPrompt(string text, int duration) {
		GetComponent<Text>().enabled = true;
		yield return new WaitForSeconds(duration);
        GetComponent<Text>().enabled = false;
	}
}
