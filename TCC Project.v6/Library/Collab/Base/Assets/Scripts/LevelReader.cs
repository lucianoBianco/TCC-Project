using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReader : MonoBehaviour {
	
	public GameObject playerJohanna, asobrab;
	public PuzzleLuz Puzzle1, Puzzle2;
	public TommyBoom Puzzle3;
	public PuzzleLuz Puzzle4, Puzzle5;
	public PlayerMotor charLib;
	private CharacterSwitch charSwitch;
	public Light lt;

	// Use this for initialization
	void Start () {
		RenderSettings.ambientIntensity = PlayerPrefs.GetFloat("ambientIntensity");
		lt.intensity = PlayerPrefs.GetFloat("directionalIntensity");
		Cursor.visible = false;
		charSwitch = GetComponent<CharacterSwitch>();
		asobrab.GetComponent<AsobrarbMovement>().SetLocation(PlayerPrefsX.GetVector3("AsobrabPos"), PlayerPrefs.GetInt("AsobrabTracker"));
		playerJohanna.transform.position = PlayerPrefsX.GetVector3("JohannaPos");
		if(PlayerPrefsX.GetBool("Puzzle1Complete")){
			Puzzle1.CompletePuzzle();
		}if(PlayerPrefsX.GetBool("Puzzle2Complete")){
			Puzzle2.CompletePuzzle();
		}if(PlayerPrefsX.GetBool("Puzzle3Complete")){
			Puzzle3.CompletePuzzle();
		}if(PlayerPrefsX.GetBool("Puzzle4Complete")){
			Puzzle4.CompletePuzzle();
		}if(PlayerPrefsX.GetBool("Puzzle5Complete")){
			Puzzle5.CompletePuzzle();
		}

		if(PlayerPrefsX.GetBool("TommyLiberado")){
			charLib.puzzleBlocked = false;
			charSwitch.LiberarTommy();
		}
	}
	
	
}
