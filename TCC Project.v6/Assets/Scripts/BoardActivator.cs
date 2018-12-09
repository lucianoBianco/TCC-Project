using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardActivator : MonoBehaviour {

	//controlador obsoleto do board em 2d
	public string botao;
	bool active = false;
	GameObject note;
	Material oldColorMat;
	public Material newMat;
	BoardController controller;
	Renderer rend;
	bool pressed;
	public AudioSource missclick, erroNota;
	void Start () {
		rend = GetComponent<MeshRenderer> ();
		oldColorMat = rend.material;
		controller = GetComponentInParent<BoardController> ();
	}

	void Update () {
		if(Input.GetButtonDown(botao)){
			Pressed ();
			if(active){
				active = false;
				AddScore ();
			}else{
				ErroMissClick();
			}
		}
		else if(Input.GetButtonUp(botao))
			Unpressed();
	}
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

	void Pressed(){
		pressed = true;
		rend.material = newMat;
		//note.GetComponent<MeshRenderer> ().material = newMat;
	}

	void Unpressed(){
		//note.GetComponent<MeshRenderer> ().material = oldColorMat;
		rend.material = oldColorMat;
		StopAllCoroutines();
		if(note != null)
			Destroy (note);
		pressed = false;
	}

	void AddScore(){
		if(note.GetComponent<Note>().isLong){
			StartCoroutine(LongPress());
			
		}else {
			if(note != null)
				Destroy (note);
		}
		controller.points += 10;
		print (controller.points);

	}
	IEnumerator LongPress(){
		while(true){
			controller.points += 1;
			yield return new WaitForSeconds(0.2f);
		}
	}

	void ErroNota(){
		erroNota.Play();
	}

	void ErroMissClick(){
		missclick.Play();
	}
}
