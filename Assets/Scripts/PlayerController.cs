using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

public class PlayerController : MonoBehaviour {

    private float speed = 6;
    private float jumpHeight = 6;
    private float turnSmoothing = 10;

	private Vector3 moveDirection = Vector3.zero;

	private CharacterController controller;
    private Animator animator;

	void Start () {
        // animator = GetComponent<Animator> ();
        controller = GetComponent<CharacterController>();
	}

	void Update () {
		if(controller.isGrounded) {
            // Getting camera relative movement
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = Camera.main.transform.TransformDirection(moveDirection.x, 0, moveDirection.z);
	        moveDirection.y = 0;

            moveDirection = moveDirection * speed;

            // Animation - when we have them
            if(moveDirection.magnitude <= 0) {
                // animator.SetBool("Walk", false);
            }
            if(moveDirection.magnitude > 0) {
                // animator.SetBool("Walk", true);
            }

			// Jump
			if (Input.GetButtonDown("Jump") || XCI.GetButtonDown(XboxButton.A))
                moveDirection.y = jumpHeight;

			// Rotate the character to match the movement direction
	        Vector3 newDirection = new Vector3(moveDirection.x, 0, moveDirection.z);
			if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f) {
	                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(newDirection), Time.deltaTime * turnSmoothing);
	        }
		}

        if(GameManager.gravityReversed)
            moveDirection.y += 20f * Time.deltaTime; // Apply reversed gravity
        else
            moveDirection.y -= 20f * Time.deltaTime; // Apply gravity

        controller.Move(moveDirection * Time.deltaTime); // Apply movement to CharacterController
    }
}
