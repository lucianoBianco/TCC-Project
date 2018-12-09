using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiberarChar : MonoBehaviour {
	public PlayerMotor charLib;
	public CharacterSwitch charSwitch;
	public GameObject wall;
	// trigger que libera o tommy para ser personagem selecionavel
	void OnTriggerEnter(Collider other){
		if (other.transform.name != "Tommy" && other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active) {
			charLib.puzzleBlocked = false;
			charSwitch.LiberarTommy();
			wall.SetActive(true);
			StartCoroutine(LetThereBeLight());
		}

	}

	void OnTriggerExit(Collider other){
		return;
	}

// acende a luz do sol aos poucos
	IEnumerator LetThereBeLight(){
		while(RenderSettings.ambientIntensity < 1){
			RenderSettings.ambientIntensity += 0.5f * Time.deltaTime;
			yield return null;
		}
	}
}
