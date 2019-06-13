﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(GunController))]
public class Player : LivingEntity {

    public float moveSpeed = 10;
    public float speed;
    public Joystick joystick;
    public Joystick joystick2;
    private Touch tempTouch;
    private bool touchOn;
    Animator animator;
    bool IsPause = false;
    bool chkRun = false;
    

    public Crosshairs crosshairs;

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;

    protected override void Start() {
        base.Start();
    }

    void Awake() {
        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
        FindObjectOfType<Spawner>().OnNewWave += OnNewWave;
        animator = GetComponent<Animator>();
    }

    void OnNewWave(int waveNumber) {
        health = startingHealth;
        gunController.EquipGun(waveNumber - 1);
    }

    void Update() {
        // Movement input
        /*
		Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));
		Vector3 moveVelocity = moveInput.normalized * moveSpeed;
		controller.Move (moveVelocity);
        */
       
        Vector3 moveInput = new Vector3(joystick.Horizontal * 5, GetComponent<Rigidbody>().velocity.y, joystick.Vertical * 5);
        if (joystick.Horizontal != 0||joystick.Vertical != 0)
        {
            animator.SetBool("chkRun", true);
        }
        else
        {
            animator.SetBool("chkRun", false);
        }
        //Vector3 moveVelocity = moveInput.normalized * moveSpeed;
        controller.Move(moveInput);
        // Vector3 rotation = ((int)joystick2.Horizontal * 5, 0, (int)joystick2.Vertical * 5);
        //Quaternion newRotation = Quaternion.LookRotation(rotation);
        //rigidbody.MoveRotation(newRotation);
        Vector3 rot = new Vector3(joystick2.Horizontal*10000, 0f, joystick2.Vertical * 10000);
        //transform.LookAt(rot);
        if (joystick2.Horizontal != 0 || joystick2.Vertical != 0)
        {
            controller.LookAt(rot);
            crosshairs.transform.position = rot;
            gunController.Aim(rot);
        }
        
        Vector3 rot2 = new Vector3(joystick2.Direction.x*100, 0f, joystick2.Direction.y*100);

        

        // Look input
        /*
        Ray ray = viewCamera.ScreenPointToRay (Input.mousePosition);
		Plane groundPlane = new Plane (Vector3.up, Vector3.up * gunController.GunHeight);
		float rayDistance;

		if (groundPlane.Raycast(ray,out rayDistance)) {
			Vector3 point = ray.GetPoint(rayDistance);
			//Debug.DrawLine(ray.origin,point,Color.red);
			controller.LookAt(point);
			crosshairs.transform.position = point;
			crosshairs.DetectTargets(ray);
			if ((new Vector2(point.x, point.z) - new Vector2(transform.position.x, transform.position.z)).sqrMagnitude > 1) {
				gunController.Aim(point);
			}
		}*/

        // Weapon input
        /*
        if (Input.GetMouseButton(0)) {
			gunController.OnTriggerHold();
		}
		if (Input.GetMouseButtonUp(0)) {
			gunController.OnTriggerRelease();
		}*/
        if (joystick2.Horizontal* joystick2.Vertical != 0)
        {
            gunController.OnTriggerHold();
            gunController.OnTriggerRelease();
        }
        if (joystick2.Horizontal == 0 && joystick2.Vertical == 0)
        {
            gunController.OnTriggerRelease();
        }

        if (Input.GetKeyDown (KeyCode.R)) {
			gunController.Reload();
		}

		if (transform.position.y < -10) {
			TakeDamage (health);
		}
	}

	public override void Die ()
	{
		AudioManager.instance.PlaySound ("Player Death", transform.position);
		base.Die ();
	}
		
}
