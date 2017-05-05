using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPanorama : MonoBehaviour {

	void Update () {

    }

	public IEnumerator Play(float time) {
		ThirdPersonController.playingPanorama = true;
		GetComponent<Image>().enabled = true;
		transform.Translate(Vector3.left * Time.deltaTime);
		yield return new WaitForSeconds(time);
		ThirdPersonController.playingPanorama = false;
	}
}
