using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	public CameraControl cam;
	public Character[] character;
	int currentChar = 0;
	int numChars;
	public int Speed;
	void Start()
	{
		numChars = character.Length;
	}
	void Update ()
	{
		if(Input.anyKey)
		{
			Character curChar = character[currentChar];
			Transform past = curChar.transform;
			if(Input.GetKey(KeyCode.UpArrow))
			{
				curChar.animation.Play();
				curChar.controller.Move(Vector3.forward * Speed);
				curChar.transform.rotation = Quaternion.Slerp(past.rotation , Quaternion.Euler(new Vector3(0,0,0)), 1.0f);
			}
			if(Input.GetKey(KeyCode.DownArrow))
			{
				curChar.animation.Play();
				curChar.controller.Move(Vector3.back * Speed);
				curChar.transform.rotation = Quaternion.Slerp(past.rotation , Quaternion.Euler(new Vector3(0,180,0)), 1.0f);
			}
			if(Input.GetKey(KeyCode.LeftArrow))
			{
				curChar.animation.Play();
				curChar.controller.Move(Vector3.left * Speed);
				curChar.transform.rotation = Quaternion.Slerp(past.rotation , Quaternion.Euler(new Vector3(0,-90,0)), 1.0f);
			}
			if(Input.GetKey(KeyCode.RightArrow))
			{
				curChar.animation.Play();
				curChar.controller.Move(Vector3.right * Speed);
				curChar.transform.rotation = Quaternion.Slerp(past.rotation , Quaternion.Euler(new Vector3(0,90,0)), 1.0f);
			}
			if(Input.GetKeyDown(KeyCode.Space))
			{
				SwitchChar();
			}
			if(Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
			{
				curChar.Attack();
			}
		}
		cam.setCamera(character[currentChar]);
	}
	void SwitchChar()
	{
		if(numChars > 1)
		{
			currentChar++;
			if(currentChar >= character.Length)
				currentChar=0;
		}
	}
	public void KillMe(Character Char)
	{
		if(numChars <= 1)
		{
			Application.LoadLevel("Win Screen");
			return;
		}
		SwitchChar();
		Destroy(Char.gameObject, 1);
		numChars--;
	}
}