using UnityEngine;
using System.Collections;

public class TutorialScript : MonoBehaviour {
	private float m_displayTime;
	
	// Use this for initialization
	void Start () {
		if(!PlayerPrefs.HasKey("firstplay")){
			m_displayTime=5f;
		//	PlayerPrefs.SetInt("firstplay", 1);
			foreach(Transform tf in transform)
				tf.gameObject.SetActive(true);
		}else{
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(m_displayTime<0){
			Destroy(gameObject);
		}else{
			m_displayTime=m_displayTime-Time.deltaTime;
		}
	
	}
}
