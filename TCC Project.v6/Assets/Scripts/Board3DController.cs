using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Board3DController : MonoBehaviour {


//gerenciador do board da cachoeira
	public GameObject[] gos;
	private Animator[] anims;
	private int index;
	public int points;
	public float animationDelay = 1.4f;
	public GameObject cachos1, cachos2, cachos3, cachos4;

	public GlobalVariablesCaverna vars;
	private AudioSource source;
	bool playing = false;


	void Start(){
        vars = GameObject.Find("WorldManager").GetComponent<GlobalVariablesCaverna>();
		source = GetComponent<AudioSource> ();
		gos = new GameObject[GameObject.FindGameObjectsWithTag("notas3D").Length];
		gos = GameObject.FindGameObjectsWithTag("notas3D");
		anims = new Animator[gos.Length];

		for (int i = 0; i < gos.Length; i++){
			anims[i] = gos[i].GetComponent<Animator>();
		}

		cachos1 = Resources.FindObjectsOfTypeAll<classe1>()[0].gameObject;
		cachos2 = Resources.FindObjectsOfTypeAll<classe2>()[0].gameObject;
		cachos3 = Resources.FindObjectsOfTypeAll<classe3>()[0].gameObject;
		cachos4 = Resources.FindObjectsOfTypeAll<classe4>()[0].gameObject;

	}

	void Update(){
		//mostra o progresso da musica atraves da quantitade de particulas na cachoeira
		if(points == 100){
			cachos4.SetActive(true);
		}else if(points > 75){
			cachos3.SetActive(true);
		}else if(points > 50){
			cachos2.SetActive(true);
		}else if(points > 25){
			cachos1.SetActive(true);
		}
	}

	IEnumerator PlayMusic(){
		//inicia o midi player e, apos um delay inicia o audio source
		float delay = animationDelay;
		while(delay > 0){
			delay -= Time.deltaTime;
			yield return null;
		}
		source.Play();
		playing = true;
	}

	public void OnStart(){
		StartCoroutine(PlayMusic());
		index = 0;
	}

	public void OnNoteSpawn(){
		//ativa uma animação de  pedra caindo a cada vez que uma nota é tocada pelo midi player
		try{
			anims[index].SetTrigger("Play");
		}catch (Exception e){
			Debug.LogException(e);
		}
		index ++;
	}

	public void OnEnd(){
		StartCoroutine(EndDelay());
	}


	IEnumerator EndDelay(){
		float delay = animationDelay + 3f;
		while(delay > 0){
			delay -= Time.deltaTime;
			yield return null;
		}
		if(points >=100){
			vars.MusicOk ();
		}else{
			vars.MusicFail ();
		}

	}
}
