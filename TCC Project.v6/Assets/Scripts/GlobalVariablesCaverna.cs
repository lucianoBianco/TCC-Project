using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GlobalVariablesCaverna : MonoBehaviour {


//controlador geral do level 1 do jogo
	public GameObject door;
	public GameObject brokenTree;
	public GameObject board;
	public GameObject musicTree;
	public GameObject spawner;
	public GameObject boardPrefab;
	public static bool isPaused;
	public GameObject pauseMenu;
	public GameObject gameOverMenu;
	public Text textFragmento;
	public AudioSource backgroundMusic;
	private BarkManager[] managers;
	private bool lockgame = false;
	public int prioridadeQuestAtual = 0;
	public CinemachineVirtualCamera Vcam;
	public BarkManager barkBoardManager;
	public Dialogue barkBoardFail;

	public bool playingSong = false;
	public Interactibles boardTrigger;
	void Start(){
		managers = Resources.FindObjectsOfTypeAll<BarkManager>();

	}
	void Update(){

//pausa o jogo
		if(!lockgame){
			if (Input.GetKeyDown(KeyCode.Escape)) {
				isPaused = !isPaused;
				Cursor.visible = !Cursor.visible;
				pauseMenu.SetActive (!pauseMenu.activeInHierarchy);
			}
		}
//reduz o volume da musica de background durante o board musical 
		if (playingSong) {
			backgroundMusic.volume -= 0.2f * Time.deltaTime;
		} else {
			if (backgroundMusic.volume < 0.5f) 
				backgroundMusic.volume += 0.2f * Time.deltaTime;
		}
	}

//da play no board musical
	public void Play(){
		if (playingSong == false) {
			playingSong = true;
			boardTrigger.boardIsOn = true;
			TravarBarks(false);
			Instantiate (boardPrefab, spawner.transform.position, spawner.transform.rotation, spawner.transform);
			Vcam.Priority = 20;
			isPaused = true; 
		}
	}
// destroi o board musical ao final da musica
	public void Destroy(){
		TravarBarks(true);
		board = GameObject.Find(boardPrefab.name + "(Clone)");
		board.SetActive(false);
		Destroy(board);
		playingSong = false;
		boardTrigger.boardIsOn = false;
		Vcam.Priority = 5;
	}
//reflete o sucesso no término do board
	public void MusicOk(){
		Destroy();
		lockgame = true;
		isPaused = true;
		Cursor.visible = true;
		gameOverMenu.SetActive(true);
		textFragmento.text = "1";
	}

//reflete a falha ao final do board
	public void MusicFail(){
		isPaused = false;
		barkBoardManager.RemoteBark(barkBoardFail);
		door.GetComponent<Light> ().enabled = false;
		Destroy();
	}
//ativador do board musical
	public void Interact(GameObject go){
		if (!door.GetComponentInChildren<Light>().enabled) {
			door.GetComponentInChildren<Light> ().enabled = true;
			Play ();
		}
	}
// desativa os barks durante diálogos
	public void TravarBarks(bool isTravado){
		foreach (BarkManager manager in managers){
			
			manager.barkingAllowed = isTravado;
			
		}
	}
//sai do jogo
	public void Quit(){
		Application.Quit ();
	}
//volta ao menu
	public void GoToMainMenu(){
		SceneManager.LoadScene(0);
	}
//
	public void Resume(){
		isPaused = false;
		Cursor.visible = false;
		pauseMenu.SetActive (false);
	}
//botão que direciona ao site do jogo
	public void Comment(){
		Application.OpenURL ("https://tccrew.itch.io/katharsis");
	}
}
