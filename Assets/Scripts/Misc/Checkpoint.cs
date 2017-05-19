using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public bool Active = false;
    public static GameObject[] Checkpoints;

	void Start () {
		Checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint"); //Populate checkpoints array
    }

	void OnTriggerEnter (Collider col) {
		if (col.tag == "Player") {
			UpdateCheckpoints();
		}
	}

	public static Vector3 GetCurrentCheckpoint() {
		Vector3 result = new Vector3(0, 0, 0); // If no checkpoints have been activated return default spawn
        if (Checkpoints != null) {
            foreach (GameObject cp in Checkpoints) {
                if (cp.GetComponent<Checkpoint>().Active) {
                    result = cp.transform.position;
                    break;
                }
            }
        }
        return result;
    }

	private void UpdateCheckpoints () {
		foreach (GameObject cp in Checkpoints) {
            cp.GetComponent<Checkpoint>().Active = false;
        }
        Active = true;
	}
}
