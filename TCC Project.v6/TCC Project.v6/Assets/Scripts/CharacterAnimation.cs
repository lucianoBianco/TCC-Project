using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimation : MonoBehaviour {

	Animator anim;
	private PlayerMotor motor;


	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
		motor = GetComponent<PlayerMotor>();
	}
	
	// Update is called once per frame
	void Update () {
		float speedPercent = motor.movAmount;
		anim.SetFloat ("SpeedPercentage", speedPercent, .1f, Time.deltaTime);
	}
}
