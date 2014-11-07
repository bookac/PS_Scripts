using UnityEngine;
using System.Collections;

public class OpenPopUpButton : MonoBehaviour {

	public GameObject PopUp;
	public GameObject BG_Dark;

	void OnClick()
	{
		PopUp.animation.Play ("PopUp_Open");
		BG_Dark.SetActive (true);
	}
}