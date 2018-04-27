using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohannaSkills : MonoBehaviour {
	public bool skill;
	bool musicMode = false;
	PlayerMotor motor;
	PlayerController controller;
	float timer, skillTimer;
	int nota;
	private AudioSource source;
	public GameObject luz;

	public AudioClip audio1, audio2, audio3;

	// Use this for initialization
	void Start () {
		controller = GetComponent<PlayerController> ();
		motor = transform.GetComponent<PlayerMotor> ();
		source = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

		switch (controller.myController) {
		case PlayerController.Controller.Active:
			timer += Time.deltaTime;
			if (Input.GetButtonDown ("Fire1")) {
				musicMode = !musicMode;
				print (musicMode);
			}
			if (musicMode)
				MusicMode ();

			skill = Input.GetButtonDown ("Ação3");
			if (skill && motor.lockOn) {
				Activate ();
			}
			break;
		}
		if(luz.GetComponent<Light> ().range>=0)
			luz.GetComponent<Light> ().range -= .2f * Time.deltaTime;

	}



	void MusicMode()
	{



		if(nota == 0 && Input.GetButton("Number1"))
		{
			nota = 1;
			timer = 0;
			source.PlayOneShot (audio1);
		}
		else if (nota == 1 && Input.GetButton("Number2"))
		{
			nota = 2;
			timer = 0;
			source.PlayOneShot (audio2);
		}
		else if (nota == 2 && Input.GetButton("Number3"))
		{
			nota = 0;
			source.PlayOneShot (audio3);
			timer = 0;
			Activate ();
		}



		if (timer >= 60)
		{
			musicMode = !musicMode;
		}
		else if (timer >= 10)
		{
			nota = 0;
		}
	}



	public void Activate(){
		luz.GetComponent<Light> ().range = 10;
	}
}
