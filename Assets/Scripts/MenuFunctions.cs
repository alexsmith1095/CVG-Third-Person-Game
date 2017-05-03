using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class MenuFunctions : MonoBehaviour {

    public GameObject buttonsPanel;
    public GameObject controlsPanel;

	void Start () {
        controlsPanel.SetActive(false);
        buttonsPanel.SetActive(true);
	}

	void Update() {
		if(XCI.GetButtonDown(XboxButton.A)) {
			ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, null, ExecuteEvents.submitHandler);
        }

		if (controlsPanel.activeInHierarchy && XCI.GetButtonDown(XboxButton.B) ||
			controlsPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Return)) {
				ShowButtons();
			}
	}

	public void HoverSound() {
        AudioManager.Main.PlayNewSound("rollover2");
    }

	public void ClickSound() {
		AudioManager.Main.PlayNewSound("rollover1");
    }

	public void SelectButton(GameObject _go) {
        EventSystem.current.SetSelectedGameObject(_go);
    }

	public void Scene(int index){
        SceneManager.LoadScene(index, LoadSceneMode.Single);
    }

	public void ShowControls () {
        controlsPanel.SetActive(true);
		buttonsPanel.SetActive(false);
	}

	public void ShowButtons () {
		buttonsPanel.SetActive(true);
		controlsPanel.SetActive(false);
	}

	public void Quit()
    {
		#if UNITY_EDITOR
        	UnityEditor.EditorApplication.isPlaying = false;
		#else
        	Application.Quit ();
		#endif
    }

}
