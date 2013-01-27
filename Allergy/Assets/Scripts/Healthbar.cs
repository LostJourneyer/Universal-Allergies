using UnityEngine;
using System.Collections;

public class Healthbar : MonoBehaviour
{
	public Character character;
	void OnGUI()
	{
		float healthbarSize = character.HitPoints * 0.1f;
		guiTexture.transform.localScale = (new Vector3(healthbarSize, 0, 1));
	}
}
