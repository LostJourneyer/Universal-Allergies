using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour
{
	public Texture[] TextureList;
	
	int size, current = 0;
	void Start()
	{
		size = TextureList.Length;
	}
	
	void OnGUI()
	{
		if(GUI.Button(new Rect(0, 0, Screen.width, Screen.height), TextureList[current]))
		{
			if(current == size-1)
				Application.LoadLevel("Level1");
			current++;
		}
	}
}
