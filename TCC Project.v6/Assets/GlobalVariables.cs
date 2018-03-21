﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour {

	public bool lock1 = false;
	public bool lock2 = false;
	public bool lock3 = false;
	public bool opened = false;

	public GameObject door1;
	public GameObject door2;
	public GameObject door3;


	private Light light;
	void Start(){
		

	}

	public void OpenDoor(){
		if (lock1 && lock2 && lock3) {
			print ("YAY");
			opened = true;
		}
	}


	public void Interact(GameObject go){
		if (!opened) {
			if (go == door1) {
				light = door1.GetComponent<Light> ();
				light.enabled = true;
				lock1 = true;
				light = door2.GetComponent<Light> ();
				light.enabled = false;
				lock2 = false;
				light = door3.GetComponent<Light> ();
				light.enabled = false;
				lock3 = false;
				print ("Lock1 On");
			} else if (go == door2) {
				if (lock1) {
					print ("Lock2 On");
					lock2 = true;
					light = door2.GetComponent<Light> ();
					light.enabled = true;
				} else {
					light = door1.GetComponent<Light> ();
					light.enabled = false;
					lock1 = false;
					light = door2.GetComponent<Light> ();
					light.enabled = false;
					lock2 = false;
					light = door3.GetComponent<Light> ();
					light.enabled = false;
					lock3 = false;
					print ("PEH");
				}
			} else if (go == door3) {
				if (lock2) {
					print ("Lock3 On");
					light = door3.GetComponent<Light> ();
					light.enabled = true;
					lock3 = true;
					OpenDoor ();
				} else {
					light = door1.GetComponent<Light> ();
					light.enabled = false;
					lock1 = false;
					light = door2.GetComponent<Light> ();
					light.enabled = false;
					lock2 = false;
					light = door3.GetComponent<Light> ();
					light.enabled = false;
					lock3 = false;
					print ("PEH");
				}
			}
		}
	}
}
