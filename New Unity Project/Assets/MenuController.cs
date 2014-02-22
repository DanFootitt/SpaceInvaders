using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {
	
	Font bitFont;
	Texture2D blank;
	
	// Use this for initialization
	void Start () {
		bitFont = Resources.Load("8-BIT WONDER") as Font;
		blank = Resources.Load("blank") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI()
	{
		
		// Button Style
		GUIStyle btnStyle = new GUIStyle(GUI.skin.button);
		
		// Set font and fontsize
		btnStyle.font = bitFont;
		btnStyle.fontSize = 30;
		
		// Change color
		btnStyle.normal.textColor = Color.white;
		btnStyle.active.textColor = Color.red;
		btnStyle.hover.textColor = Color.green;
		
		// Change background
		btnStyle.normal.background = blank;
		btnStyle.active.background = blank;
		btnStyle.hover.background = blank;
		
		// Play Game Button
		if (GUI.Button(new Rect(460 - (400/2),300,400,50), "New Game", btnStyle))
		{
			Application.LoadLevel("Main");
		}
		// High Scores Button
		if (GUI.Button(new Rect(460 - (400/2),360,400,50), "High Scores", btnStyle))
		{
			Application.LoadLevel("Main");
		}
		
		// Instructions Button
		if (GUI.Button(new Rect(460 - (400/2),420,400,50), "Instructions", btnStyle))
		{
			Application.LoadLevel("Main");
		}
		
		
	}
}
