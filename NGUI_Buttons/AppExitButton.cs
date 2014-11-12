using UnityEngine;
using System.Collections;

public class AppExitButton : MonoBehaviour {

	void OnClick()
	{
		Application.Quit();
		transform.animation.Play ("Button_Pressed");
	}
}


