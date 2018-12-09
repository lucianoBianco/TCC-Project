using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour {

//obsoleto
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
		anim.SetFloat ("SpeedPercentage", speedPercent, .1f, Time.deltaTime);
	}
}
