using UnityEngine;
using System.Collections;

public class MenuScript : MonoBehaviour
{
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "Play Game"))
			Application.LoadLevel("Scene1");
	}
}
