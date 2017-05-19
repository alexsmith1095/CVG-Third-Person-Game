using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killbox : MonoBehaviour {

    private GameObject camera;

	void Start () {
        camera = Camera.main.gameObject;
	}

	void OnTriggerEnter (Collider col) {
		if(col.tag == "Player") {
            camera.GetComponent<ThirdPersonCamera>().StartCoroutine("OnFall");
            GameManager.deathCount += 1;
		}
	}
}
