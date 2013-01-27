using UnityEngine;
using System.Collections;

public class HighScores : MonoBehaviour {
	private static bool sm_updateScoreNames;
	private static int sm_place;
//	private GUIText[] scoreTexts;
	private string stringToEdit;
	public Texture buttonTexture;
	public Font scoreFont;

	// Use this for initialization
	void Start () {
//		scoreTexts = new GUIText[10];
		stringToEdit = "";
		//Display Stuff goes here
		// initialize the texts in the array
//		for(int h=0; h<10; h++){
	//		scoreTexts[h].text = GUI.Label (new Rect(10, 50+(20*h), 500, 20), "");
	//		scoreTexts[h].font = scoreFont;
//		}
		
		for(int i=0; i<10; i++){
			Debug.Log(PlayerPrefs.GetString("u_playerName"+i)+" "+ PlayerPrefs.GetInt("u_playerScore"+i));
		}
	}
	
	// Update is called once per frame
	void Update () {
		//use for navagation back
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
				return;
			}
		}
		sm_updateScoreNames= false;
	}
	void OnGUI()
	{
		if(sm_updateScoreNames)
		{
			// Make a text field that modifies stringToEdit.
	    	stringToEdit = GUI.TextField (new Rect (10, 10, 200, 20), stringToEdit, 25);
		
	        if (GUI.Button(new Rect(220, 10, 50, 30), new GUIContent("Enter", buttonTexture)))
	        {
				PlayerPrefs.SetString("u_playerName"+sm_place, stringToEdit);
				stringToEdit = "";
	        }
		}
		DisplayScores();
//	    if (GUI.Button(new Rect(280, 10, 80, 30), new GUIContent("Test Data", buttonTexture)))
//		{
//			createDummyTestData();
//	    }
	}

	void DisplayScores()
	{
		int top_offset = 20; // space on top for text box
		int titlezone = 20; // vertical space for title of table
		int boundary = 5; // space around table
		int titlex = boundary; // x offset for title
		int titley = top_offset; // y offset for title
		int boxwidth = (int)(camera.pixelWidth/2 - boundary); // width of one table cell
		int boxheight = (int)((camera.pixelHeight - top_offset - titlezone - 2*boundary) / 10); // height of one table cell
		int nameoriginoffsetx = boundary; // x offset of first name cell
		int nameoriginoffsety = top_offset + titlezone + boundary; // y offset of first name cell
		int scoreoriginoffsetx = boundary+boxwidth; // x offset of first score cell
		int scoreoriginoffsety = top_offset + titlezone + boundary; // y offset of first score cell

		GUI.Label (new Rect(titlex, titley, camera.pixelWidth - 2*boundary, titlezone), "High Scores");
		for(int h = 0; h < 10; h++)
		{
//			string text = (PlayerPrefs.GetString("u_playerName"+h)+" "+ PlayerPrefs.GetInt("u_playerScore"+h)+"\n");
			//Debug.Log("display scores text: "+"u_playerName"+h+", u_playerScore"+h+": " + text);
//			GUI.Label(new Rect(10, 50+(20*h), 500, 20), text);
			
			string nametext = PlayerPrefs.GetString("u_playerName"+h);
			string scoretext = (""+PlayerPrefs.GetInt("u_playerScore"+h));
			
			GUI.Label (new Rect(nameoriginoffsetx, nameoriginoffsety + h*boxheight, boxwidth, boxheight), nametext);
			GUI.Label (new Rect(scoreoriginoffsetx, scoreoriginoffsety + h*boxheight, boxwidth, boxheight), scoretext);
		}
	}

	void createDummyTestData()
	{
	//	int randomNumber = Random.Range(0, 1000);
	//	Debug.Log(randomNumber);
	//	SetHighScore(randomNumber);
	}
}