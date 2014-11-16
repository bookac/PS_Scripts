using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class WebCamControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Initialize(){
		Debug.Log("Initialize");
		//set up camera
		WebCamDevice[] devices = WebCamTexture.devices;
		string backCamName="";
		for( int i = 0 ; i < devices.Length ; i++ ) {
			Debug.Log("Device:"+devices[i].name+ "IS FRONT FACING:"+devices[i].isFrontFacing);

			if (!devices[i].isFrontFacing) {
				backCamName = devices[i].name;
			}
		}			     
	}
}
