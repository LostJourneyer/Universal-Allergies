using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour {
	private static bool sm_updateScoreNames;
	private static int sm_place;
	public Texture buttonTexture;
	public Font scoreFont;
	private string playerName="";
	private TapLevelLoader tll;
#if UNITY_IPHONE
	TouchScreenKeyboard keyboard;
#endif

	// Use this for initialization
	void Start () {
		sm_place=PlayerPrefs.GetInt("Player");
		if(PlayerPrefs.GetInt("Update")==1)
			sm_updateScoreNames=true;
		else
			sm_updateScoreNames=false;
#if UNITY_IPHONE
		keyboard = TouchScreenKeyboard.Open(playerName,TouchScreenKeyboardType.Default);
#endif
	}

	// Update is called once per frame
	void Update () {
		//use for navagation back
	}
	void OnGUI()
	{
#if !UNITY_IPHONE
		if(sm_updateScoreNames)
		{
			// Make a text field that modifies stringToEdit.
	    	playerName = GUI.TextField (new Rect(Screen.width*1/4,Screen.height*1/4, Screen.width*1/2, Screen.height*1/16), playerName, 25);

	        if (GUI.Button(new Rect(Screen.width*1/4,Screen.height*1/2,  Screen.width*1/2,Screen.height*1/8), new GUIContent("Enter", buttonTexture)))
	        {
				PlayerPrefs.SetString("u_playerName"+sm_place, playerName);
				sm_updateScoreNames=false;
				if(tll==null){
					tll=(TapLevelLoader)gameObject.AddComponent<TapLevelLoader>();
					tll.m_SceneToLoad="Opening";
				}
	        }
		}else{
			if(tll==null){
				tll=(TapLevelLoader)gameObject.AddComponent<TapLevelLoader>();
				tll.m_SceneToLoad="Opening";
			}
	}
		DisplayScores();
#else
		if(sm_updateScoreNames)
		{
			if (keyboard.active){
				playerName = keyboard.text;
			}
			if(keyboard.done)
			{
				PlayerPrefs.SetString("u_playerName"+sm_place, playerName);
				sm_updateScoreNames=false;
				if(tll==null){
					tll=(TapLevelLoader)gameObject.AddComponent<TapLevelLoader>();
					tll.m_SceneToLoad="Opening";
				}
				sm_updateScoreNames=false;
	        }
		}else{
			if(tll==null){
				tll=(TapLevelLoader)gameObject.AddComponent<TapLevelLoader>();
				tll.m_SceneToLoad="Opening";
			}
		}
		DisplayScores();
		
		
#endif
	}

	void DisplayScores()
	{
		int top_offset = 20; // space on top for text box
		int titlezone = 40; // vertical space for title of table
		int boundary = 18; // space around table
		int titlex = boundary; // x offset for title
		int titley = top_offset; // y offset for title
		int nameboxwidth = (int)(camera.pixelWidth*0.75 - boundary); // width of one table cell
		int nameboxheight = (int)((camera.pixelHeight - top_offset - titlezone - 2*boundary) / 10); // height of one table cell
		int scoreboxwidth = (int)(camera.pixelWidth/4 - boundary); // width of one table cell
		int scoreboxheight = (int)((camera.pixelHeight - top_offset - titlezone - 2*boundary) / 10); // height of one table cell

		int nameoriginoffsetx = boundary; // x offset of first name cell
		int nameoriginoffsety = top_offset + titlezone + boundary; // y offset of first name cell
		int scoreoriginoffsetx = boundary+nameboxwidth; // x offset of first score cell
		int scoreoriginoffsety = top_offset + titlezone + boundary; // y offset of first score cell

				GUI.backgroundColor = Color.yellow;


		GUI.skin.font = scoreFont;
		GUI.skin.label.fontSize = 36;
		var centeredStyle = GUI.skin.GetStyle("Label");
    	centeredStyle.alignment = TextAnchor.UpperCenter;
//		GUI.Label (new Rect(titlex, titley, camera.pixelWidth - 2*boundary, titlezone), "High Scores");


		GUI.skin.label.fontSize = 18;
		for(int h = 0; h < 10; h++)
		{
//			string text = (PlayerPrefs.GetString("u_playerName"+h)+" "+ PlayerPrefs.GetInt("u_playerScore"+h)+"\n");
			//Debug.Log("display scores text: "+"u_playerName"+h+", u_playerScore"+h+": " + text);
//			GUI.Label(new Rect(10, 50+(20*h), 500, 20), text);

			string nametext = PlayerPrefs.GetString("u_playerName"+h);
			string scoretext = (""+PlayerPrefs.GetInt("u_playerScore"+h));

			var leftStyle = GUI.skin.GetStyle ("Label");
			leftStyle.alignment = TextAnchor.LowerLeft;
			GUI.Label (new Rect(nameoriginoffsetx, nameoriginoffsety + h*nameboxheight, nameboxwidth, nameboxheight), nametext);

			var rightStyle = GUI.skin.GetStyle ("Label");
			rightStyle.alignment = TextAnchor.LowerRight;
			GUI.Label (new Rect(scoreoriginoffsetx, scoreoriginoffsety + h*scoreboxheight, scoreboxwidth, scoreboxheight), scoretext);
		}
	}
	
	public static void SetHighScore(int score)
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
				sm_place=i;
				Debug.Log("sm_place = "+sm_place);
				sm_updateScoreNames= true;
				PlayerPrefs.SetInt("Update", 1);
				PlayerPrefs.SetInt("Player",i);
				return;
			}
		}
		PlayerPrefs.SetInt("Update", 0);
		sm_updateScoreNames= false;
	}
}