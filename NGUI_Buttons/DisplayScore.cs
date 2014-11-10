using UnityEngine;
using System.Collections;

public class DisplayScore : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<UILabel>().text += PlayerPrefs.GetInt("SCORE").ToString();
	}
}
