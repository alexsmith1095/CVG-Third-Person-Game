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
        if (XCI.GetNumPluggedCtrlrs() <= 0) {
            Cursor.lockState = CursorLockMode.None; // Unlock the cursor
            Cursor.visible = true; // Show the cursor
        }
    }

    public void ResumeGame() {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
        pausePanel.SetActive(false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<ThirdPersonController>().enabled = true;
        Camera.main.gameObject.GetComponent<ThirdPersonCamera>().enabled = true;
    }

    IEnumerator Intro () {
        playingIntro = true;
        Cursor.lockState = CursorLockMode.Locked; // Lock the cursor
        Cursor.visible = false; // Hide the cursor
        yield return new WaitForSeconds (5);
        playingIntro = false;
    }
}
