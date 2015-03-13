using UnityEngine;
using System.Collections;

// Controls pipe creation and desctruction.
public class PipeControl : MonoBehaviour
{
	public static float currentX;
	public float verticalVariation;
	public int rearrangeDelay;
	bool seen = false;
	GameObject go;

	// Use this for initialization
	void Start ()
	{
		if (GetComponent<Renderer> () == null) {
			Debug.LogWarning("no renderer attached, visibility events won't be triggered");
		}
		setRandomY (verticalVariation);
		go = transform.gameObject;
		//Debug.LogFormat ("pipe start ({0}) at {1}", getId (), Time.time);
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	}
	
	void OnBecameInvisible ()
	{
		//Debug.LogFormat ("pipe invisible ({0}) at {1}", getId (), Time.time);
		if (seen) {
			Invoke("rearrange", rearrangeDelay);
		}
	}
	
	void rearrange ()
	{
		float x = currentX + CameraTracksPlayer._pipeDistance;
		setX (x);
		seen = false;
	}
	
	void OnBecameVisible ()
	{
		//Debug.LogFormat ("pipe seen ({0}) at {1}", getId (), Time.time);
		seen = true;
	}
	
	void setX (float x)
	{
		//Debug.LogFormat ("pipe new x: {0} #{1} - curr x: {2}", x, getId (), currentX);
		Vector3 pos = transform.position;
		pos.x = x;
		transform.position = pos;
		currentX = pos.x;
	}
	
	void setRandomY (float variant)
	{
		Vector3 pos = transform.position;
		//Debug.LogFormat ("pipe y: {0} #{1}", pos.y, getId ());
		pos.y += Random.Range (-variant, variant);
		transform.position = pos;
	}
	
	int getId ()
	{
		if (go == null) {
			Debug.Log ("go is null, can't get id");
			return -1;
		}
		return go.GetInstanceID ();
	}
}
