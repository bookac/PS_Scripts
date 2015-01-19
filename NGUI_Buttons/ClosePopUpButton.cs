using UnityEngine;
using System.Collections;

public class ClosePopUpButton : MonoBehaviour {

	public GameObject PopUp;
	public GameObject BG_Dark_Sprite;
	
	void OnClick()
	{
		PopUp.animation.Play ("PopUp_Close");
		BG_Dark_Sprite.SetActive (false);
	}
}
