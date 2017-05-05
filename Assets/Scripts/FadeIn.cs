using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    private float alphaFadeValue = 1;
    public Texture blackTexture;

	void Update () {
		alphaFadeValue -= Mathf.Clamp01(Time.deltaTime / 3);
	}

	void OnGUI () {
		GUI.color = new Color(0, 0, 0, alphaFadeValue);
		GUI.DrawTexture( new Rect(0, 0, Screen.width, Screen.height ), blackTexture );
	}
}
