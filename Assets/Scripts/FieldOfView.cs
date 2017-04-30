using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour {

	[Range(0, 360)]
	public float angle;
    public float radius;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();


	void Start () {
		StartCoroutine("FindTargetsWithDelay", .2f);
	}

	IEnumerator FindTargetsWithDelay (float delay) {
		while (true) {
            yield return new WaitForSeconds (delay);
			FindTargets();
		}
	}

	void FindTargets () {
        visibleTargets.Clear(); // Avoid duplicates of targets
		// Store all targets within viewing radius using the target layer mask
		Collider[] targetsInRange = Physics.OverlapSphere(transform.position, radius, targetMask);

		// Loop through the list of targets and check if they are in view
		foreach (Collider col in targetsInRange) {
			Transform target = col.transform;
			Vector3 targetDirection = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, targetDirection) < angle / 2) {
                float targetDistance = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast(transform.position, targetDirection, targetDistance, obstacleMask)) {
					// The target is in view and not blocked by an obstacle
					visibleTargets.Add(target);
				}
			}
		}
    }

	public Vector3 DirectionFromAngle (float angleInDegrees, bool angleIsGlobal) {
		if (!angleIsGlobal) {
            angleInDegrees += transform.eulerAngles.y;
        }
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


}
