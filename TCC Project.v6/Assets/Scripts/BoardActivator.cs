using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardActivator : MonoBehaviour {
	public string botao;
	bool active = false;
	GameObject note;
	Material oldColorMat;
	public Material newMat;
	BoardController controller;
	Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<MeshRenderer> ();
		oldColorMat = rend.material;
		controller = GetComponentInParent<BoardController> ();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown (botao))
			StartCoroutine (Pressed ());
		if (Input.GetButtonDown (botao) && active) {
			Destroy (note);
			active = false;
			AddScore ();
		}
	}
	void OnTriggerEnter(Collider col){
		active = true;
		if (col.gameObject.tag == "Note") {
			note = col.gameObject;
		}
	}
	void OnTriggerExit(Collider col){
		active = false;
	}

	IEnumerator Pressed(){
		rend.material = newMat;
		yield return new WaitForSeconds (0.1f);
		rend.material = oldColorMat;

	}

	void AddScore(){
		controller.points += 10;
		print (controller.points);

	}
}
