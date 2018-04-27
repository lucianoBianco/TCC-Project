using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {
	Rigidbody rb;
	float newY;
	public GlobalVariables vars;

	void Awake(){
	}

	// Use this for initialization
	void Start () {
		newY = transform.localPosition.y;
        vars = GameObject.Find("World State").GetComponent<GlobalVariables>();
	}
	
	// Update is called once per frame
	void Update () {
		if (vars.playingSong) {
			newY -= 1 * Time.deltaTime;
			Vector3 pos = transform.localPosition;
			pos.y = newY;
			transform.localPosition = pos;
		}
	}
}
