using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	void OnGUI()
	{
		if(GUI.Button(new Rect(Screen.width/2 - 100, Screen.height/2 - 50, 100, 50), "Start Game"))
			Application.LoadLevel("Level1");
	}
}
