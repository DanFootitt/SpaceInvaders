using UnityEngine;
using System.Collections;

public class GameOverControllerScript : MonoBehaviour {
	
	
	GameObject player;
	Font bitFont;
	Texture2D blank;
	string pName = "";
	
	// Use this for initialization
	void Start () {
		
		player = GameObject.FindGameObjectWithTag("Player");
		if (player != null) player.SetActive(false);
		
		
		bitFont = Resources.Load("8-BIT WONDER") as Font;
		blank = Resources.Load("blank") as Texture2D;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (pName.Length < 5)
		{
			if (Input.GetKeyDown(KeyCode.A)) 	pName += "A";
			if (Input.GetKeyDown(KeyCode.B)) 	pName += "B";
			if (Input.GetKeyDown(KeyCode.C)) 	pName += "C";
			if (Input.GetKeyDown(KeyCode.D)) 	pName += "D";
			if (Input.GetKeyDown(KeyCode.E)) 	pName += "E";
			if (Input.GetKeyDown(KeyCode.F)) 	pName += "F";
			if (Input.GetKeyDown(KeyCode.G)) 	pName += "G";
			if (Input.GetKeyDown(KeyCode.H)) 	pName += "H";
			if (Input.GetKeyDown(KeyCode.I)) 	pName += "I";
			if (Input.GetKeyDown(KeyCode.J)) 	pName += "J";
			if (Input.GetKeyDown(KeyCode.K)) 	pName += "L";
			if (Input.GetKeyDown(KeyCode.L)) 	pName += "L";
			if (Input.GetKeyDown(KeyCode.M)) 	pName += "M";
			if (Input.GetKeyDown(KeyCode.N)) 	pName += "N";
			if (Input.GetKeyDown(KeyCode.O)) 	pName += "O";
			if (Input.GetKeyDown(KeyCode.P)) 	pName += "P";
			if (Input.GetKeyDown(KeyCode.Q)) 	pName += "Q";
			if (Input.GetKeyDown(KeyCode.R)) 	pName += "R";
			if (Input.GetKeyDown(KeyCode.S)) 	pName += "S";
			if (Input.GetKeyDown(KeyCode.T)) 	pName += "T";
			if (Input.GetKeyDown(KeyCode.U)) 	pName += "U";
			if (Input.GetKeyDown(KeyCode.V)) 	pName += "V";
			if (Input.GetKeyDown(KeyCode.W)) 	pName += "W";
			if (Input.GetKeyDown(KeyCode.X)) 	pName += "X";
			if (Input.GetKeyDown(KeyCode.Y)) 	pName += "Y";
			if (Input.GetKeyDown(KeyCode.Z)) 	pName += "Z";

		}
		
		if (pName.Length > 0)
		{
			if (Input.GetKeyDown(KeyCode.Backspace)) 	pName = pName.Substring(0, pName.Length - 1);
			if (Input.GetKeyDown(KeyCode.Return)) 		Application.LoadLevel("MainMenu");
		}
		
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
		if (GUI.Button(new Rect(460 - (400/2),100,400,50), "GAME OVER", btnStyle))
		{
		}

		
		if (player != null)
		{	Player p = player.GetComponent<Player>();
			if (GUI.Button(new Rect(460 - (600/2),300,600,50), "Player Score : " + p.getScore().ToString(), btnStyle))
			{
			}
		}
		
		if (GUI.Button(new Rect(460 - (400/2),500,400,50), pName, btnStyle))
		{
		}
		
		
	}
}
