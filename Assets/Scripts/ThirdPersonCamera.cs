using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class ThirdPersonCamera : MonoBehaviour {

    private Transform target;
    private Transform fallingTarget;
    private float distance = 4f;
    private float maxDistance = 6f;
    private float x = 0.0f;
    private float y = 0.0f;
    private float xSpeed = 80f;
    private float ySpeed = 100f;
    private bool playingIntro;

	void Start () {
        Cursor.lockState = CursorLockMode.Locked; // Hide the cursor
        target = GameObject.FindGameObjectWithTag("Player").transform;
        fallingTarget = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 angles = transform.eulerAngles;
        x = angles.x;
        y = angles.y;
        StartCoroutine("Intro");
    }

	void LateUpdate () {
        if(target) {
            // Offset Y to avoid focusing on feet
            Vector3 targetOffset = new Vector3(target.position.x, (target.position.y + 1f), target.position.z);

            if (!playingIntro) {
                // Rotation
                if (XCI.GetNumPluggedCtrlrs() > 0) {
                    x += XCI.GetAxis(XboxAxis.RightStickX) * xSpeed * distance * 0.02f;
                    y -= XCI.GetAxis(XboxAxis.RightStickY) * ySpeed * 0.02f;
                } else {
                    x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                    y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
                }

                // Zoom
                if (distance < maxDistance) {
                    if(XCI.GetDPadDown(XboxDPad.Down) || Input.GetKeyDown(KeyCode.Minus)) {
                        distance += 1.5f;
                        xSpeed -= 18f;
                    }
                } else if (distance >= maxDistance) {
                    if(XCI.GetDPadDown(XboxDPad.Down) || Input.GetKeyDown(KeyCode.Minus)) {
                        distance -= 3f;
                        xSpeed += 36f;
                    }
                }
            }

            y = Mathf.Clamp(y, -25, 80); // Clamp rotation to avoid flipping

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 position = targetOffset - (rotation * Vector3.forward * distance);

            WallCollisions(targetOffset, ref position);

            transform.position = position;
            transform.rotation = rotation;
        } else {
            StartCoroutine("OnFall");
        }
    }

    private void WallCollisions(Vector3 fromPlayer, ref Vector3 toCamera) {

        Debug.DrawLine(fromPlayer, toCamera, Color.cyan);

        RaycastHit hit = new RaycastHit();
        if(Physics.Linecast(fromPlayer, toCamera, out hit)) {
            Debug.DrawRay(hit.point, Vector3.left, Color.red);
            if (hit.transform.gameObject.layer != LayerMask.NameToLayer("IgnoreCameraCollision")) {
                if(hit.distance < distance) {
                    toCamera = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                } else {
                    toCamera = new Vector3(hit.point.x, toCamera.y, hit.point.z);
                }
            }
        }
    }

    public IEnumerator OnFall() {
        target = null;
        transform.LookAt(fallingTarget);
        yield return new WaitForSeconds(2);
        PlayerEvents.PlayerDamaged(fallingTarget.gameObject, 5);
        target = fallingTarget;
    }

    IEnumerator Intro () {
        playingIntro = true;
        yield return new WaitForSeconds(3);
        playingIntro = false;
    }
}
