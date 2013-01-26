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
		}
	}
}
