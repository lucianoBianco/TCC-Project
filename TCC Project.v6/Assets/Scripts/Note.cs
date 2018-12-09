using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

	// script obsoleto que controla as notas no board 2d
	Rigidbody rb;
	float newY;
	public bool isLong;
	public GlobalVariablesCaverna vars;

	void Start () {
		newY = transform.localPosition.y;
        vars = GameObject.Find("WorldManager").GetComponent<GlobalVariablesCaverna>();
	}
	
	void Update () {
		if (vars.playingSong) {
			newY -= 0.32f * Time.deltaTime;
			Vector3 pos = transform.localPosition;
			pos.y = newY;
			transform.localPosition = pos;
		}
	}
}
