using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;
using UnityEngine.SceneManagement;

public class TreasureChest : MonoBehaviour {

    public GameObject scorePanel;
    private bool hasPlayer;
    private bool complete;

    void Start () {
        scorePanel.SetActive(false);
    }

	void Update () {
		if(hasPlayer && Input.GetKeyDown(KeyCode.E) || hasPlayer && XCI.GetButtonDown(XboxButton.Y)) {
			StartCoroutine("CompleteGame");
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.tag == "Player") {
            hasPlayer = true;
            if(XCI.GetNumPluggedCtrlrs() > 0)
			    PlayerEvents.DisplayPrompt("Press Y to Collect Treasure", 100);
            else
                PlayerEvents.DisplayPrompt("Press E to Collect Treasure", 100);
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
        complete = true;
        PlayerEvents.DisplayPrompt("1000G Achievement Unlocked! - Completed the game", 5);
        yield return new WaitForSeconds(2);
        scorePanel.SetActive(true);
        yield return new WaitForSeconds(8);
        scorePanel.SetActive(false);
		SceneManager.LoadScene(0, LoadSceneMode.Single);
	}
}
