using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

//marcador de pontos do board obsoleto em 2d
	public int points;
	public GlobalVariablesCaverna vars;
	private AudioSource source;
	bool playing = false;

    void Start()
    {
        vars = GameObject.Find("WorldManager").GetComponent<GlobalVariablesCaverna>();
		source = GetComponent<AudioSource> ();
		StartCoroutine(PlayMusic());
    }


    void Update(){
    	Debug.Log(source.isPlaying);
		if (!source.isPlaying && points >= 100) {
			vars.MusicOk ();
		} else if (!source.isPlaying && playing) {
			vars.MusicFail ();
		}
	}

	public void Destruir(){
		Destroy (gameObject);
	}
	IEnumerator PlayMusic(){
		float delay = 5f;
		while(delay > 0){
			delay -= Time.deltaTime;
			yield return null;
		}
		source.Play();
		playing = true;
	}
}
