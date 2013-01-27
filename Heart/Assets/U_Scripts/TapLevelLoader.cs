using UnityEngine;
using System.Collections;

public class TapLevelLoader : MonoBehaviour {
	
	public string m_SceneToLoad;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKey||Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1)){
			Application.LoadLevel(m_SceneToLoad);
		}
	}
}
