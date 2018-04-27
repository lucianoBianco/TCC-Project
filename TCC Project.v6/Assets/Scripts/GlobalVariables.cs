using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

	public bool lock1 = false;
	public bool lock2 = false;
	public bool lock3 = false;
	public bool opened = false;

	public GameObject door1;
	public GameObject door2;
	public GameObject door3;
    public GameObject tree;
    public GameObject brokenTree;
	public GameObject board;
	public GameObject musicTree;
	public GameObject spawner;
	public GameObject boardPrefab;


	public bool playingSong = false;

	private Light light;
	void Start(){
		

	}

	public void OpenDoor(){
		if (lock1 && lock2 && lock3) {
			print ("YAY");
			opened = true;
            Destroy(tree);
            Instantiate(brokenTree,tree.transform.position, tree.transform.rotation);
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

	public void Interact(GameObject go){
		if (!opened) {
			if (go == door1) {
				light = door1.GetComponent<Light> ();
				light.enabled = true;
				lock1 = true;
				light = door2.GetComponent<Light> ();
				light.enabled = false;
				lock2 = false;
				light = door3.GetComponent<Light> ();
				light.enabled = false;
				lock3 = false;
			} else if (go == door2) {
				if (lock1) {
					lock2 = true;
					light = door2.GetComponent<Light> ();
					light.enabled = true;
				} else {
					light = door1.GetComponent<Light> ();
					light.enabled = false;
					lock1 = false;
					light = door2.GetComponent<Light> ();
					light.enabled = false;
					lock2 = false;
					light = door3.GetComponent<Light> ();
					light.enabled = false;
					lock3 = false;
				}
			} else if (go == door3) {
				if (lock2) {
					light = door3.GetComponent<Light> ();
					light.enabled = true;
					lock3 = true;
					OpenDoor ();
				} else {
					light = door1.GetComponent<Light> ();
					light.enabled = false;
					lock1 = false;
					light = door2.GetComponent<Light> ();
					light.enabled = false;
					lock2 = false;
					light = door3.GetComponent<Light> ();
					light.enabled = false;
					lock3 = false;
				}
			}
		}
	}
}
