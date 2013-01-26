using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	void Start ()
	{
	}
	void Update ()
	{
		if(Input.anyKey)
		{
			if(Input.GetKey(KeyCode.UpArrow))
				transform.position += Vector3.up;
			if(Input.GetKey(KeyCode.DownArrow))
				transform.position += Vector3.down;
			if(Input.GetKey(KeyCode.LeftArrow))
				transform.position += Vector3.left;
			if(Input.GetKey(KeyCode.RightArrow))
				transform.position += Vector3.right;
		}
	}
}
