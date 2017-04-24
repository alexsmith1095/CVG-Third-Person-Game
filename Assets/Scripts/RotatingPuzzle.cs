using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;

/// <Summary>
/// Attach this script to each object you want to be rotated.
/// </Summary>
[RequireComponent(typeof(BoxCollider))]
public class RotatingPuzzle : MonoBehaviour {

	public enum Solution { Up = 0, Right = 90, Down = 180, Left = 270 }; // The solution rotation
    public Solution solution;
    public float rotSpeed = 6f;
    public float rotationIncrement = 90f;

    private bool hasPlayer = false; // Is the player in range
    private Vector3 targetRotation; // Euler that we want to rotate to
    private bool solved;
    private bool complete;
    private GameObject door;
    private Vector3 doorPosition;
    private Vector3 newDoorPosition;

    public GameObject[] rotatingPuzzleItems;

	void Start () {
        rotatingPuzzleItems = GameObject.FindGameObjectsWithTag("RotatingPuzzleItem");
        targetRotation = transform.rotation.eulerAngles;
		door = GameObject.Find("RotatingPuzzleDoor");
        doorPosition = door.transform.position;
    }

	void Update () {
        Rotation();
	}

	void OnTriggerEnter() {
        hasPlayer = true; // Player is inside trigger
		if(XCI.GetNumPluggedCtrlrs() > 0)
			PlayerEvents.DisplayPrompt("Press X to Rotate", 100);
		else
			PlayerEvents.DisplayPrompt("Press E to Rotate", 100);
	}

	void OnTriggerExit() {
        hasPlayer = false; // Player has left the trigger
		PlayerEvents.DisplayPrompt("", 100); // Remove the on-screen control prompt
    }

	/// <summary>
	///	Handle the user input and rotation of the object.
	/// </summary>
	private void Rotation () {
		if (hasPlayer && (Input.GetKeyDown(KeyCode.E) || XCI.GetButtonDown(XboxButton.X))) {
			targetRotation.z = Increment(targetRotation.z);
			// Check for solution
			if(targetRotation.z == (float)solution) {
            	this.solved = true;
            } else {
				this.solved = false;
			}
			CheckForSolution();
        }
		// Apply smooth rotation to the object
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime * rotSpeed);
	}

	/// <summary>
	///	Increment the rotation axis by the specified amount.
	/// </summary>
	private float Increment(float axis) {
		axis += rotationIncrement;
		if(axis >= 360f) {
			axis = 0;
        }
		return axis;
	}

	/// <summary>
	///	Check all the puzzle items in the scene for a solution.
	/// If all have been solved, unlock the puzzle.
	/// </summary>
	void CheckForSolution() {

		int solvedCount = 0;

		for (int i = 0; i < rotatingPuzzleItems.Length; i++) {
			if(rotatingPuzzleItems[i].GetComponent<RotatingPuzzle>().solved == true) {
                solvedCount++;
			}
        }

		if(solvedCount >= rotatingPuzzleItems.Length) {
            complete = true;
		} else {
			complete = false;
    	}

		if(complete) {
			door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 10f, door.transform.position.z);
        } else {
			door.transform.position = new Vector3(doorPosition.x, doorPosition.y, doorPosition.z);
		}
	}
}
