using UnityEngine;
using System.Collections;

public class SolarWind : MonoBehaviour {
	private float life=5f;
	void Update()
	{
		Debug.Log("sol");
		life=life-Time.deltaTime;
		if(life<0){
			Destroy(gameObject);
		}
	}
}
