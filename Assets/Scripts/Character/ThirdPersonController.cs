using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

[RequireComponent(typeof(CharacterController))]
public class ThirdPersonController : MonoBehaviour {

    public float speed = 6f;
    public float sprintMultiplier = 1.5f;
    public float jumpForce = 10f;
    public float turnSpeed = 6f;
    public Transform door;

    private Vector3 moveDirection;
    private Vector3 movementInput;
	private Vector3 cameraForward;
    private CharacterController controller;
    private Animator animator;
    private Camera camera;
	public GameObject contextIntroPanel;
    public static bool playingContextIntro; // The text explaining the games context
    private bool playingIntro; // The player animation walking into the cave

    void OnEnable () {
        moveDirection = Vector3.zero;
    }

	void Start  () {
		controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        camera = Camera.main;
		StartCoroutine ("ContextIntro");
        StartCoroutine("Intro");
    }

	void Update () {
        if (!playingIntro)
            Movement();
    }

    void FixedUpdate () {
        if(!GameManager.gravityReversed)
            moveDirection.y -= 30f * Time.deltaTime;
        else
            moveDirection.y += 30f * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    void Movement () {
        movementInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        cameraForward = Vector3.Scale(camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 lookDirection = new Vector3(moveDirection.x, 0, moveDirection.z);

        if (controller.isGrounded) {
            moveDirection = movementInput.x * camera.transform.right + movementInput.z * cameraForward;
            moveDirection *= speed;

            // Animation
            animator.SetFloat("Speed", moveDirection.magnitude / 10);

			if (Input.GetButton ("Jump") || XCI.GetButton (XboxButton.A)) {
				moveDirection.y = jumpForce;
			}

        } else {
            // If !grounded we don't want the Y velocity to be recalculated
            moveDirection.x = (movementInput.x * camera.transform.right + movementInput.z * cameraForward).x * speed / 1.2f;
            moveDirection.z = (movementInput.x * camera.transform.right + movementInput.z * cameraForward).z * speed / 1.2f;
        }

        // Rotate the character to movement direction
        if (lookDirection != Vector3.zero) {
            if (controller.isGrounded)
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * turnSpeed);
            else // Slower turning in mid air
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * turnSpeed / 1.5f);
        }
    }

	// Display a paragraph of text introducing the game for a few seconds
	IEnumerator ContextIntro () {
		playingContextIntro = true;
		contextIntroPanel.SetActive (true);
		yield return new WaitForSeconds (10);
		contextIntroPanel.SetActive (false);
		playingContextIntro = false;
		yield break;
	}

	// Animate the character walking into the cave and disable player controls
    IEnumerator Intro () {
        while(playingContextIntro)
             yield return new WaitForSeconds(0.1f); // Pause the coroutine until weve read the context intro
        playingIntro = true;
        moveDirection = Vector3.forward * 1.2f;
        animator.SetFloat("Speed", .2f); // Make the player walk forward
        yield return new WaitForSeconds(10);
        moveDirection = Vector3.zero;
        animator.SetFloat("Speed", 0f); // Stop Walking
        door.position = new Vector3(door.position.x, door.position.y - 3.1f, door.position.z);
        playingIntro = false;
		yield break;
    }
}
