using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyTarget : MonoBehaviour {


// controla as animações dos objetos que podem ser explodidos

	private Animator anim;
	public bool liberarCondicao;
	public CameraController cam;
	public GameObject particles;

	void Start () {
		anim = GetComponent<Animator>();
	}


// ativa as animações de sucesso e falha do objeto
	public void Succeed(){
		anim.SetInteger("Condition", 2);
		Reset();
	}

	public void Fail(){
		anim.SetInteger("Condition", 1);
		Reset();
	}


// repara o objeto em caso de falha
	public void ResetPuzzle(){
		anim.SetInteger("Condition", 0);
		gameObject.tag = "FocusObj";
		foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
			if(player.GetComponent<SwitchFocus>() != null)
				player.GetComponent<SwitchFocus>().AddFocus(gameObject);
			
		}
	}
	public void Complete(){
		StartCoroutine(CompleteDelay());
	}

	IEnumerator CompleteDelay(){
		int i = 10;
		while(i>0){
			i--;
			yield return null;
		}
		Succeed();
	}
	
	
	// remove o bjeto da lista de alvos possiveis do tommy caso tenha sido destruido
	void Reset(){
		foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
			player.GetComponent<PlayerController>().ResetLockon();
			if(player.GetComponent<SwitchFocus>() != null)
				player.GetComponent<SwitchFocus>().RemoveFocus(gameObject);
			gameObject.tag = "Ground";
		}
	}

	public void BarkFeedback(){
		GetComponent<BarkFeedbackManager>().FeedbackBark(liberarCondicao);
	}

// define se a particula de mira está ativa
	void Update(){
		if(transform.gameObject == cam.lockonTarget){
			particles.SetActive(true);
		}
		else{
			particles.SetActive(false);
		}
	}


}
