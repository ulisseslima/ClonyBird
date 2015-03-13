using UnityEngine;
using System.Collections;

public class BGLooper : MonoBehaviour
{
	int numBGPanels = 6;
	GameObject pipes;

	void OnTriggerEnter2D (Collider2D collider)
	{
		float widthOfBGObject = ((BoxCollider2D)collider).size.x;

		Vector3 pos = collider.transform.position;
		pos.x += widthOfBGObject * numBGPanels - widthOfBGObject / 2;
		collider.transform.position = pos;
	}

	void Update ()
	{
		 
	}
}
