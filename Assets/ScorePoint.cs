using UnityEngine;
using System.Collections;

public class ScorePoint : MonoBehaviour {
	AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == "Player") {
			audio.Play();
			Score.addPoint();
		}
	}
}
