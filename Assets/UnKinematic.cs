using UnityEngine;
using System.Collections;

public class UnKinematic : MonoBehaviour {
	Rigidbody2D body;

	void Start () {
		body = GetComponent<Rigidbody2D> ();
	}
	
	void Update () {
	}

	void OnCollisionEnter2D(Collision2D collision) {
		if (body == null){
			Debug.LogError ("no body");
			return;
		}
		body.isKinematic = false;
	}
}
