using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariablesCaverna : MonoBehaviour {


	public GameObject brokenTree;
	public GameObject board;
	public GameObject musicTree;
	public GameObject spawner;
	public GameObject boardPrefab;
	public static bool isPaused;
	public GameObject pauseMenu;

	public bool playingSong = false;
	void Start(){


	}
	void Update(){
		if (Input.GetButtonDown ("Pause")) {
			isPaused = !isPaused;
			pauseMenu.SetActive (!pauseMenu.activeInHierarchy);
		}
	}
	public void Play(){
		if (playingSong == false) {
			playingSong = true;
			Instantiate (boardPrefab, spawner.transform.position, spawner.transform.rotation, spawner.transform);
		}
	}

	public void Destroy(){
		board = GameObject.Find("BoardMusical(Clone)");
		board.GetComponent<BoardController> ().Destruir ();
		playingSong = false;
	}

	public void MusicOk(){
		StartCoroutine (Destruir());
	}
	IEnumerator Destruir(){
		Destroy (musicTree);
		Instantiate(brokenTree, musicTree.transform.position, musicTree.transform.rotation);
		yield return new WaitForSeconds(3f);
		Destroy ();
	}
}
