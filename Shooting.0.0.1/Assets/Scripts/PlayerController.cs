using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour {

	Vector3 velocity;
	Rigidbody myRigidbody;
    Animator animator;
    bool chkRun = false;

	void Start () {
		myRigidbody = GetComponent<Rigidbody> ();
        animator = GetComponent<Animator>();
    }

	public void Move(Vector3 _velocity) {
		velocity = _velocity;
        if (velocity.sqrMagnitude != 0)
        {
            chkRun = true;
        }
        else
        {
            chkRun = false;
        }
	}

	public void LookAt(Vector3 lookPoint) {
		Vector3 heightCorrectedPoint = new Vector3 (lookPoint.x, transform.position.y, lookPoint.z);
		transform.LookAt (heightCorrectedPoint);
	}

	void FixedUpdate() {
		myRigidbody.MovePosition (myRigidbody.position + velocity * Time.fixedDeltaTime);

	}
}
