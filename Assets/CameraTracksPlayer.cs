using UnityEngine;
using System.Collections;

public class CameraTracksPlayer : MonoBehaviour
{
	public static float _pipeDistance;
	public int pipePoolSize; // 10
	public float pipeDistance; // 1
	public float pipeXStart; // 1
	Transform player;
	float offsetX;

	// Use this for initialization
	void Start ()
	{
		GameObject player_go = GameObject.FindGameObjectWithTag ("Player");

		if (player_go == null) {
			Debug.LogError ("Couldn't find an object with tag 'Player'!");
			return;
		}

		player = player_go.transform;
		offsetX = transform.position.x - player.position.x;

		_pipeDistance = pipeDistance;
		
		for (int i = 1; i <= pipePoolSize; i++) {
			GameObject pipeWall = Instantiate (Resources.Load ("Pipes")) as GameObject;
			incrementX (pipeWall, pipeDistance * (i + pipeXStart));

//			Debug.LogFormat ("created pipe: {0} #{1} - curr x: {2}", 
//			                 pipeWall.transform.position.x, 
//			                 pipeWall.GetInstanceID (),
//			                 PipeControl.currentX);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (player != null) {
			Vector3 pos = transform.position;
			pos.x = player.position.x + offsetX;
			transform.position = pos;
		}
	}

	void incrementX (GameObject go, float n)
	{
		Vector3 pos = go.transform.position;
		pos.x += n;
		go.transform.position = pos;
		PipeControl.currentX = pos.x;
	}
}
