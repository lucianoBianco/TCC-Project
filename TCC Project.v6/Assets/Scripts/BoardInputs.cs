using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInputs : MonoBehaviour {
	public GlobalVariables vars;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("M")) {
			vars.Play ();
		}

		if (Input.GetButtonDown ("N")) {
			vars.Destroy ();
		}
	}
}
