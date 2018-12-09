using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReader : MonoBehaviour {
	//carrega o jogo a partir do progresso salvo ou definido no menu inicial
	public GameObject playerJohanna, asobrab, playerTommy;
	public PuzzleLuz Puzzle1, Puzzle2;
	public TommyBoom Puzzle3;
	public PuzzleLuz Puzzle4, Puzzle5;
	public PlayerMotor charLib;
	private CharacterSwitch charSwitch;

	void Start () {
		RenderSettings.ambientIntensity = PlayerPrefs.GetFloat("ambientIntensity");
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
			playerTommy.transform.position = PlayerPrefsX.GetVector3("TommyPos");
		}
	}
	
	
}
