using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Display Stuff goes here
		for(int i=0; i<10; i++){
			Debug.Log(PlayerPrefs.GetString("u_playerName"+i)+" "+ PlayerPrefs.GetInt("u_playerScore"+i));
		}
	}
	
	// Update is called once per frame
	void Update () {
		//use for navagation back
	}
	public static bool SetHighScore(int score)
	{
		if(!PlayerPrefs.HasKey("u_playerScore"+0)){
			for(int i=0; i<10; i++){
				PlayerPrefs.SetString("u_playerName"+i, "God");
				PlayerPrefs.SetInt("u_playerScore"+i, 0);
			}
		}
		
		for(int i=0; i<10; i++){
			if(PlayerPrefs.GetInt("u_playerScore"+i)<score){
				for(int x=9; x>i;x--){
					PlayerPrefs.SetInt("u_playerScore"+x, PlayerPrefs.GetInt("u_playerScore"+(x-1)));
					PlayerPrefs.SetString("u_playerName"+x, PlayerPrefs.GetString("u_playerName"+(x-1)));
				}
				PlayerPrefs.SetInt("u_playerScore"+i, score);
				PlayerPrefs.SetString("u_playerName"+i, getName());
				return true;
			}
		}
		return false;
	}
}
