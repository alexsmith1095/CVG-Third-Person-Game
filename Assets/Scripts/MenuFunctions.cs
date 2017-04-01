using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using XboxCtrlrInput;

public class MenuFunctions : MonoBehaviour {

	void Update() {
		if(XCI.GetButtonDown(XboxButton.A)) {
			ExecuteEvents.Execute(EventSystem.current.currentSelectedGameObject, null, ExecuteEvents.submitHandler);
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
		SceneManager.LoadScene(index);
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
