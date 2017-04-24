using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class TreasureChest : MonoBehaviour {

    private bool hasPlayer;
	private bool complete;

	void Update () {
		if(hasPlayer && Input.GetKeyDown(KeyCode.E) || hasPlayer && XCI.GetButtonDown(XboxButton.X)) {
			PlayerEvents.DisplayPrompt("1000G Achievement Unlocked! - Complete the game", 5);
			StartCoroutine("CompleteGame");
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
            hasPlayer = true;
			PlayerEvents.DisplayPrompt("Press X to Collect Treasure", 100);
		}
    }

	void OnTriggerExit (Collider col) {
		if  (col.tag == "Player") {
			hasPlayer = false;
			if (!complete)
				PlayerEvents.DisplayPrompt("", 100);
		}
	}

	IEnumerator CompleteGame () {
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
