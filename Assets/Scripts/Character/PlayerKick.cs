using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerKick : MonoBehaviour {

    public LayerMask other;
	private Animator animator;
    FieldOfView fov;

	void Start () {
        animator = GetComponent<Animator>();
		fov = GetComponent<FieldOfView>();
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.Q) || XCI.GetButtonDown(XboxButton.X)) {
            animator.SetTrigger("Kick");
			if (fov.visibleTargets.Count > 0) {
                Rigidbody rb = fov.visibleTargets[fov.visibleTargets.Count - 1].GetComponent<Rigidbody>();
				rb.AddForceAtPosition(transform.forward * 10f, rb.transform.position - transform.position, ForceMode.Impulse);
			}
		}
	}
}
