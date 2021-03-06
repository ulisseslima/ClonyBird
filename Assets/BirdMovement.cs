﻿using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour
{
	static int LEFT_BUTTON = 0;
	public float flapSpeed = 150f; //150
	public float forwardSpeed = 1f;
	Animator animator;
	AudioSource audio;
	Rigidbody2D body;
	bool didFlap = false;
	bool dead = false;
	bool visible;
	float deathCooldown = .5f;
	int flapCount = 0;

	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody2D> ();
		animator = transform.GetComponentInChildren<Animator> ();
		audio = GetComponent<AudioSource> ();
	}
	// called once per frame
	void Update ()
	{
		if (dead) {
			deathCooldown -= Time.deltaTime;

			if (deathCooldown <= 0) {
				if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (LEFT_BUTTON)) {
					Application.LoadLevel (Application.loadedLevel);
				}
			}
		} else if (visible) {
			if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (LEFT_BUTTON)) {
				didFlap = true;
			}
		}
	}

	void trigger (string triggerName)
	{
		if (animator != null) {
			animator.SetTrigger (triggerName);
			// Debug.Log ("triggered: "+triggerName + " ("+flapCount+")");
		} else
			Debug.LogError ("couldn't trigger " + triggerName + ", animator is undefined");
	}

	// called once per 50th second (or something like that)
	void FixedUpdate ()
	{
		if (dead)
			return;

		Debug.Log ("up force: " + body.velocity.y);

		body.AddForce (Vector2.right * forwardSpeed);

		if (didFlap) {
			body.AddForce (Vector2.up * flapMechanics ());
			flapCount++;
			triggerFlap();
			didFlap = false;
		}

		if (body.velocity.y > 0) {
			transform.rotation = Quaternion.Euler (0, 0, 30);
		} else {
			float angle = Mathf.Lerp (30, -90, (-body.velocity.y / 3f));
			transform.rotation = Quaternion.Euler (0, 0, angle);
		}
	}

	void OnCollisionEnter2D (Collision2D collision)
	{
		trigger ("Death");
		dead = true;
	}

	void OnBecameInvisible ()
	{
		visible = false;
	}

	void OnBecameVisible ()
	{
		visible = true;
	}

	float flapMechanics ()
	{
		float flapFactor = body.velocity.y < 0 ? flapSpeed : flapSpeed / 2;
		if (flapFactor > flapSpeed)
			flapFactor = flapSpeed;
		if (flapFactor < flapSpeed / 2)
			flapFactor = flapSpeed / 2;
		return flapFactor;
	}

	void triggerFlap() {
		trigger ("DoFlap");
		audio.Play ();
	}
}
