using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public static string pp_hiscore = "hiscore";
	static int score = 0;
	static int hiscore = 0;
	private Text text;

	static public void addPoint ()
	{
		score++;
		if (score > hiscore) {
			hiscore = score;
		}
	}

	// Use this for initialization
	void Start ()
	{
		score = 0;
		hiscore = PlayerPrefs.GetInt (pp_hiscore, 0);
		text = GetComponent <Text> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		text.text = string.Format ("Score: {0}\nHi Score: {1}", score, hiscore);
	}

	void OnDestroy() {
		PlayerPrefs.SetInt (pp_hiscore, hiscore);
	}
}
