using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public GameObject optionsMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	public void Quit(){
		Application.Quit ();
	}


	public void GameStart(){
		SceneManager.LoadScene ("Caverna");
	}

	public void OptionMenu(){
		optionsMenu.SetActive (!optionsMenu.activeInHierarchy);
	}
}
