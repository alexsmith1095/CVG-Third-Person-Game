using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliderHit : MonoBehaviour {

	float pushPower = 2.0f;
    float weight = 6.0f;

    void OnControllerColliderHit (ControllerColliderHit col) {
        if(col.gameObject.tag == "BreakableTile")
            col.gameObject.SendMessage("RecieveMessage", col.gameObject.GetInstanceID());

        // Physics push
        Rigidbody rb = col.collider.attachedRigidbody;
        Vector3 force;

        if (rb == null || rb.isKinematic) { return; }

        if (col.moveDirection.y < -0.3) {
            force = new Vector3 (0, -0.5f, 0) * 20f * weight;
        } else {
            force = col.controller.velocity * pushPower;
        }

        rb.AddForceAtPosition(force, col.point);
    }
}
