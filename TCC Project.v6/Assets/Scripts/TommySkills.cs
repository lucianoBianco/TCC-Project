using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommySkills : MonoBehaviour {


	// controlador das skills do Tommy
	public bool skill;
	bool musicMode = false;
	PlayerMotor motor;
	PlayerController controller;
	GameObject target;
	public GameObject puzzlemaster;
	float timer;
	int nota;
	public GameObject world;
	int skillcounter;
	private AudioSource source;
	public AudioClip audio1, audio2, audio3;
	public GameObject guitarra;
	private Animator anim;


	void Start () {
		motor = transform.GetComponent<PlayerMotor> ();
		controller = GetComponent<PlayerController> ();
		source = GetComponent<AudioSource> ();
		anim = transform.GetComponentInChildren<Animator>();
		
	}
	
// registra os inputs
	void Update () {

		switch (controller.myController) {
		case PlayerController.Controller.Active:
			timer += Time.deltaTime;
			if (Input.GetButtonDown ("Fire1")) {
				musicMode = true;
				timer = 0;
				guitarra.SetActive (true);
				anim.SetBool("GuitarOn", true);
			}
			if (musicMode)
				MusicMode ();

			skill = Input.GetButtonDown ("Ação3");
			if (skill && motor.lockOn) {
				Activate ();
			}
			break;
		}
	}


// libera os inputs das notas musicais e registra se uma skill foi ativada
	void MusicMode()
	{
		if(Input.GetButtonDown("Number3")){
			nota = 1;
			timer = 0;
			source.PlayOneShot (audio1, .3f);
			anim.SetTrigger("Play1");
		}else if(Input.GetButtonDown("Number1")){
			if(nota == 1 &&  motor.lockonTarget != null){
				nota = 2;
			} else{
				nota = 0;
			}
			timer = 0;
			source.PlayOneShot (audio2, .3f);
			anim.SetTrigger("Play2");
		}else if(Input.GetButtonDown("Number2")){
			if(nota == 2 &&  motor.lockonTarget != null){
				nota = 0;
				Activate ();
			}
			timer = 0;
			source.PlayOneShot (audio3, .3f);
			anim.SetTrigger("Play3");
		}
		if (timer >= 8)
		{
			musicMode = false;
			guitarra.SetActive (false);
			anim.ResetTrigger("Play3");
			anim.ResetTrigger("Play2");
			anim.ResetTrigger("Play1");
			anim.SetBool("GuitarOn", false);
		}
		else if (timer >= 10)
		{
			nota = 0;
		}
	}


// ativa a skill de explosão
	public void Activate(){
		skill = false;
		motor.lockOn = false;
		target = motor.lockonTarget;
		puzzlemaster.GetComponent<TommyBoom> ().Boom (target);
		musicMode = false;
		StartCoroutine (DelayHideInstrument ());
	}
// esconde o instrumento após um tempo
	IEnumerator DelayHideInstrument(){
		yield return new WaitForSeconds (3f);
		guitarra.SetActive (false);
		anim.SetBool("GuitarOn", false);
	}
}
