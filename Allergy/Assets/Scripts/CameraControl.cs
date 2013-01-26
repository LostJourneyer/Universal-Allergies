using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	public void setCamera(Character character)
	{
		transform.position = new Vector3(character.transform.position.x, character.transform.position.y-50.0f, -100.0f);
	}
}
