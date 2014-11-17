using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WebCamControl : MonoBehaviour {

	WebCamTexture camObj;

	// Use this for initialization
	void Start () {
		camObj = new WebCamTexture();
		Initialize();
	}

	public void Initialize(){
		Debug.Log("Initialize");
		camObj.Play();
	}
}
