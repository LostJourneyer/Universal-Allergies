using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public CameraControl cam;
	public Character[] character;
	int currentChar = 0;
	void Update ()
	{
		if(Input.anyKey)
		{
			if(Input.GetKey(KeyCode.UpArrow))
				character[currentChar].transform.position += Vector3.up;
			if(Input.GetKey(KeyCode.DownArrow))
				character[currentChar].transform.position += Vector3.down;
			if(Input.GetKey(KeyCode.LeftArrow))
				character[currentChar].transform.position += Vector3.left;
			if(Input.GetKey(KeyCode.RightArrow))
				character[currentChar].transform.position += Vector3.right;
			if(Input.GetKeyDown(KeyCode.Space))
			{
				currentChar++;
				if(currentChar >= character.Length)
					currentChar=0;
			}
		}
		cam.setCamera(character[currentChar]);
	}
}
