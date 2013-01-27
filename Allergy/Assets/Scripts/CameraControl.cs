using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	public void setCamera(Character character)
	{
		transform.position = new Vector3(character.transform.position.x, 50.0f, character.transform.position.z - 50);
	}
}
