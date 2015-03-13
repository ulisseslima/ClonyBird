using UnityEngine;
using System.Collections;

public class UnKinematic : MonoBehaviour {
	Rigidbody2D body;
	AudioSource audio;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
		audio = GetComponent<AudioSource> ();
	}
	
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (body == null){
			Debug.LogError ("no body");
			return;
		}
		audio.Play();
		body.isKinematic = false;
	}
}
