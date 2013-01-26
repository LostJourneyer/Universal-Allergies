using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour
{
	public void NextCharacter(Character character)
	{
		transform.position = new Vector3(character.transform.position.x, character.transform.position.y, -10.0f);
	}
}
