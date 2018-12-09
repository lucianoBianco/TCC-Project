using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation1 : MonoBehaviour {

//define a intensidade da animação de corrida atravez do parametro speedPercent do player motor 
	Animator anim;
	private PlayerMotor motor;



	void Start () {
		anim = GetComponentInChildren<Animator>();
		motor = GetComponent<PlayerMotor>();
	}

	void Update () {
		float speedPercent = motor.movAmount;
        if (motor.movBlocked)
            speedPercent = 0f;
		anim.SetFloat ("Speed", speedPercent, .1f, Time.deltaTime);
	}
}
