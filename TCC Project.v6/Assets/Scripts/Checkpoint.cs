using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour {


	//classe que salva no PlayerPrefs e no PlayerPrefsX as posições dos personagens e os estados dos puzzles
	private bool isNear;
	public Vector3 JohannaPos, AsobrabPos, TommyPos;
	public bool Puzzle1Complete, Puzzle2Complete, Puzzle3Complete, Puzzle4Complete, Puzzle5Complete;
	public int AsobrabTracker;
	public bool liberarChar;
	public bool triggerOn;
	public bool cenarioOn;

	void OnTriggerEnter(Collider other){
		if(triggerOn){
			if(other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active){
				SaveCheck();
				isNear = true;
			}
		}
	}

	void OntriggerExit(Collider other){
		if(other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active){
			isNear = false;
		}
	}

	void Update(){
		if(isNear){
			if(Input.GetButtonDown ("Ação1")){
				RestoreLevel();
			}
		}
	}



	public void SaveCheck(){
		PlayerPrefsX.SetVector3("JohannaPos", JohannaPos);
		PlayerPrefsX.SetVector3("AsobrabPos", AsobrabPos);
		PlayerPrefsX.SetVector3("TommyPos", TommyPos);
		PlayerPrefs.SetInt("AsobrabTracker", AsobrabTracker);
		PlayerPrefsX.SetBool("Puzzle1Complete", Puzzle1Complete);
		PlayerPrefsX.SetBool("Puzzle2Complete", Puzzle2Complete);
		PlayerPrefsX.SetBool("Puzzle3Complete", Puzzle3Complete);
		PlayerPrefsX.SetBool("Puzzle4Complete", Puzzle4Complete);
		PlayerPrefsX.SetBool("Puzzle5Complete", Puzzle5Complete);
		PlayerPrefsX.SetBool("TommyLiberado", liberarChar);
		PlayerPrefsX.GetBool("GameStart", false);
		if(cenarioOn){
			PlayerPrefs.SetFloat("ambientIntensity", 1f);
			PlayerPrefs.SetFloat("directionalIntensity", 1f);
		}
		else{
			PlayerPrefs.SetFloat("ambientIntensity", 0.4f);
			PlayerPrefs.SetFloat("directionalIntensity", 0f);
		}

	}


//faz o reload do level ao ativar uma pedra de reset
	void RestoreLevel(){
		SceneManager.LoadScene ("Caverna");
	}
}
