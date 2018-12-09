using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DevExit : MonoBehaviour {
	// controles de desenvolvedor para pular diálogos e criar saves em checkpoints especificos
	private DialogManager[] dialoguesM;
	private ComplexDialogueManager[] compDialoguesM;
	public GameObject devMenu;
	public GameObject PlayerJohanna;
	
	// Use this for initialization
	void Start () {
		dialoguesM = FindObjectsOfType<DialogManager>();
		compDialoguesM = FindObjectsOfType<ComplexDialogueManager>();
		
	}
	
	
	public void SkipDialogue(){
		foreach (DialogManager diag in  dialoguesM){
			diag.deactivate = true;
		}
		
		
		foreach (ComplexDialogueManager cDiag in  compDialoguesM){
			cDiag.deactivate = true;
		}
		
	}
	
	public void ToggleDevMenu(bool valor){
		devMenu.SetActive(valor);
		
	}
	
	public void StartLevel(){
		PlayerPrefsX.SetVector3("JohannaPos", new Vector3(-3, 2.5f,5));
		PlayerPrefsX.SetVector3("AsobrabPos", new Vector3(-31.19f, 2.69f,0.27f));
		PlayerPrefs.SetInt("AsobrabTracker", 0);
		PlayerPrefsX.SetBool("Puzzle1Complete", false);
		PlayerPrefsX.SetBool("Puzzle2Complete", false);
		PlayerPrefsX.SetBool("Puzzle3Complete", false);
		PlayerPrefsX.SetBool("Puzzle4Complete", false);
		PlayerPrefsX.SetBool("Puzzle5Complete", false);
		SceneManager.LoadScene ("Caverna");
		PlayerPrefs.SetFloat("ambientIntensity", 0.4f);
	}
	
	
	public void StartCheck1(){
		PlayerPrefsX.SetVector3("JohannaPos", new Vector3(-92f, 4.3f, 54f));
		PlayerPrefsX.SetVector3("AsobrabPos", new Vector3(-110f, 10f, 71f));
		PlayerPrefs.SetInt("AsobrabTracker", 3);
		PlayerPrefsX.SetBool("Puzzle1Complete", true);
		PlayerPrefsX.SetBool("Puzzle2Complete", false);
		PlayerPrefsX.SetBool("Puzzle3Complete", false);
		PlayerPrefsX.SetBool("Puzzle4Complete", false);
		PlayerPrefsX.SetBool("Puzzle5Complete", false);
		SceneManager.LoadScene ("Caverna");
		PlayerPrefs.SetFloat("ambientIntensity", 0.4f);
	}


	public void StartCheck2(){
		PlayerPrefsX.SetVector3("JohannaPos", new Vector3(-105, 8.5f, 73));
		PlayerPrefsX.SetVector3("AsobrabPos", new Vector3(-117.8f, 10f, 80.3f));
		PlayerPrefs.SetInt("AsobrabTracker", 4);
		PlayerPrefsX.SetBool("Puzzle1Complete", true);
		PlayerPrefsX.SetBool("Puzzle2Complete", true);
		PlayerPrefsX.SetBool("Puzzle3Complete", false);
		PlayerPrefsX.SetBool("Puzzle4Complete", false);
		PlayerPrefsX.SetBool("Puzzle5Complete", false);
		SceneManager.LoadScene ("Caverna");
		PlayerPrefs.SetFloat("ambientIntensity", 0.4f);
	}

	public void StartCheck3(){
		PlayerPrefsX.SetVector3("JohannaPos", new Vector3(-105, 8.5f, 73));
		PlayerPrefsX.SetVector3("AsobrabPos", new Vector3(-117.8f, 10f, 80.3f));
		PlayerPrefs.SetInt("AsobrabTracker", 5);
		PlayerPrefsX.SetBool("Puzzle1Complete", true);
		PlayerPrefsX.SetBool("Puzzle2Complete", true);
		PlayerPrefsX.SetBool("Puzzle3Complete", true);
		PlayerPrefsX.SetBool("Puzzle4Complete", false);
		PlayerPrefsX.SetBool("Puzzle5Complete", false);
		SceneManager.LoadScene ("Caverna");
		PlayerPrefs.SetFloat("ambientIntensity", 2f);
	}
}
