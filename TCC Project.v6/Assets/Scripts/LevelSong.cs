using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSong : MonoBehaviour {

// controla a musica no level


	public AudioClip t1, l1;
	public AudioSource adSource;
	private bool transito;
	void Update () {
		 
		 if(adSource.isPlaying && !transito)
		 {
		 	adSource.clip = t1;
		 	adSource.Play();
		 	transito=true;



		 }
		 else if (!adSource.isPlaying && transito)
		 {
		 	adSource.clip = l1;
		 	adSource.Play();




		 }
	}
}
