using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class Pause : MonoBehaviour {

    private GameObject pausePanel;
    private bool playingIntro;

	void Start() {
		pausePanel = GameObject.Find("PausePanel");
        pausePanel.SetActive(false);
        StartCoroutine("Intro");
    }

    void Update() {
        if (!playingIntro) {
            if(Input.GetKeyDown (KeyCode.Escape) || XCI.GetButtonDown(XboxButton.Start)) {
    			if (!pausePanel.activeInHierarchy) {
    				PauseGame();
                } else if (pausePanel.activeInHierarchy) {
    				ResumeGame();
                }
            }
            if (XCI.GetButtonDown(XboxButton.B)) {
                if (pausePanel.activeInHierarchy)
    				ResumeGame();
            }
        }
    }

    public void PauseGame() {
		Time.timeScale = 0;
		pausePanel.SetActive(true);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().enabled = false;
        Camera.main.gameObject.GetComponent<ThirdPersonCamera>().enabled = false;
    }

    public void ResumeGame() {
		Time.timeScale = 1;
		pausePanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().enabled = true;
        Camera.main.gameObject.GetComponent<ThirdPersonCamera>().enabled = true;
    }

    IEnumerator Intro () {
        playingIntro = true;
        yield return new WaitForSeconds (5);
        playingIntro = false;
    }
}
