using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour
{
	public float flapSpeed = 150f;
	int LEFT_BUTTON = 0;
	Rigidbody2D body;
	Animator animator;
	float forwardSpeed = 1f;
	bool didFlap = false;
	bool dead = false;
	int flapCount = 0;
	float deathCooldown = .5f;

	// Use this for initialization
	void Start ()
	{
		body = GetComponent<Rigidbody2D> ();
		animator = transform.GetComponentInChildren<Animator> ();
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
		} else {
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

		body.AddForce (Vector2.right * forwardSpeed);

		if (didFlap) {
			body.AddForce (Vector2.up * flapSpeed);
			flapCount++;
			trigger ("DoFlap");
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
}
