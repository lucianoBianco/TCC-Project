using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNota3D : MonoBehaviour {


	// controlla os triggers d board 3d

	public string botaoAtivador;
	private bool active, pressed;
	private GameObject note;
	private Board3DController controller;
	private Material oldColorMat;
	public Material materialHighlight, materialSuccess, materialFailure;
	private Renderer rend;
	private bool highlight, score;

	void Start(){
		rend = GetComponent<MeshRenderer> ();
		oldColorMat = rend.material;
		controller = GetComponentInParent<Board3DController> ();
	}


	// define se o trigger deve estar com os materiais de highlight ou padrão
	void Update(){
		
		if(!highlight){
			if(active){
				rend.material = materialHighlight;
			}else{
				rend.material = oldColorMat;
			}
		}
		

		// registra se o input de nota foi dado no momento certo
		pressed = Input.GetButton(botaoAtivador);
		if(pressed){
			if(active){
				active = false;
				AddScore ();
			}else if(!score){
				ErroMissClick();
			}
		}else{
			score = false;
		}
	}

	// registra se uyma nota está em contato com o ativador
	void OnTriggerEnter(Collider col){
		active = true;
		if (col.gameObject.tag == "Note") {
			note = col.gameObject;
		}
		if(pressed){
			Destroy (note);
			active = false;
			ErroNota();
		}
	}

	void OnTriggerExit(Collider col){
		active = false;
		if(note != null)
			Destroy (note);
	}

	// soma pontos em caso de acerto e ativa o material de acerto
	void AddScore(){
		score = true;
		if(note != null)
			Destroy (note);

		controller.points += 10;
		print (controller.points);
		StartCoroutine(MatHighlight(materialSuccess));
	}

// registra o tipo de erro que aconteceu
	void ErroNota(){
		Debug.Log("ErroNota");
		StartCoroutine(MatHighlight(materialFailure));
		
	}

	void ErroMissClick(){
		Debug.Log("MissclickNota");
		StartCoroutine(MatHighlight(materialFailure));
	}
	

	// troca o material de acordo com o feedback de erro e acerto
	IEnumerator MatHighlight(Material newMat){
		highlight = true;
		rend.material = newMat;
		
		yield return new WaitForSeconds(0.25f);
		highlight = false;
	}
	
	
	
	
	
	
}
