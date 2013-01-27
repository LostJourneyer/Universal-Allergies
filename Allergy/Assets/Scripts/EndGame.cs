using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour
{
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, Screen.width, Screen.height), "\n\nPlay Again"))
			Application.LoadLevel("Menu");
	}
}
