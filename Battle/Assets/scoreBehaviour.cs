using UnityEngine;
using System.Collections;

public class scoreBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		guiText.text = "Score: " + PlayerPrefs.GetFloat ("Score");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
