using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOut : MonoBehaviour {

    private float alphaFadeValue = 0;
    public Texture blackTexture;
    public static bool complete;

	void Update () {
        if (complete)
            alphaFadeValue += Mathf.Clamp01(Time.deltaTime / 3);
	}

	void OnGUI () {
        if (complete) {
            GUI.color = new Color(0, 0, 0, alphaFadeValue);
    		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), blackTexture);
        }
	}
}
