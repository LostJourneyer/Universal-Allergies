using UnityEngine;
using System.Collections;

public class DisplayScore : MonoBehaviour {
	
	string m_lastScore;
	void Start()
	{
		m_lastScore=" "+PlayerPrefs.GetInt("u_lastScore");
	}
	void OnGUI(){
		GUI.Box(new Rect(Screen.width/4, (5*Screen.height)/8, Screen.width/2, Screen.height/8), m_lastScore);
		if (GUI.Button(new Rect(Screen.width/4, (5*Screen.height)/8, Screen.width/2, Screen.height/8), "Continue")){
			if(PlayerPrefs.GetInt("Update")==1){
				Application.LoadLevel("HighScores");
			}else{
				Application.LoadLevel("Opening");
			}
		}
	}
}
