using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	GameObject Player;
	// Use this for initialization
	void Start () {
		Player = GameObject.Find("TheRunner");
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z + 2.5f);
	}
}
