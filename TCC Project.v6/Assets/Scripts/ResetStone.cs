using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetStone : MonoBehaviour {
	// gerencia as reset stones

	private bool isNear;
	public GameObject particles;
	public GameObject hud;
	private bool isOn;
	private bool outofrange = false;

// caso o jogador esteja proximo ativa a hud de 'press E to interact'
	void OnTriggerStay(Collider other){
		if(other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active){
			isOn = true;
			outofrange = false;
		}
	}
// registra os inputs e esconde a hud caso o jogador tenha se afastado
	void Update(){
		if(isOn){
			isNear = true;
			particles.SetActive(true);
			hud.SetActive(true);
			isOn = false;
		}else{
			Deactivate();
		}
		if(isNear){
			if(Input.GetButtonDown ("Ação1")){
				RestoreLevel();
			}
		}
	}
// recarrega o level para resetar os estados dos puzzle
	void RestoreLevel(){
		SceneManager.LoadScene ("Caverna");
	}

//desativa a hud
	void Deactivate(){
		if(!outofrange){
			isNear = false;
			hud.SetActive(false);
			particles.SetActive(false);
			outofrange = true;
		}
	}
}
