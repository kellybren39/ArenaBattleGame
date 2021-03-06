﻿using UnityEngine;

public class deadBehaviour : MonoBehaviour
{
	//public GUIText scoreText;

	void Awake()
	{
		guiText.text = "Score: " + PlayerPrefs.GetFloat ("Score");
	}

	void Update(){
		guiText.text = "Score: " + PlayerPrefs.GetFloat ("Score");
	}

	void OnGUI()
	{
		guiText.text = "Score: " + PlayerPrefs.GetFloat ("Score");
		const int buttonWidth = 84;
		const int buttonHeight = 30;
		
		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		Rect buttonRect = new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3) - (3 * (buttonHeight / 2)),
			buttonWidth,
			buttonHeight
			);
		
		Rect exitRect = new Rect(
			Screen.width / 2 - (buttonWidth / 2),
			(2 * Screen.height / 3) - (buttonHeight / 5),
			buttonWidth,
			buttonHeight
			);
		
		// Draw a button to start the game
		if (GUI.Button (buttonRect, "Main Menu")) {
			// On Click, load the level.
			// "stadium" is the name of the first scene we created.
			Application.LoadLevel ("mainMenu");
		} else if (GUI.Button (exitRect, "Exit")) {
			// Exit the game
			Application.Quit();
		}
	}
}
