using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorMove : MonoBehaviour {
	public GameObject porta;
	Vector3 newPos;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		newPos = transform.position;
		newPos.y = porta.transform.position.y + 3;
		transform.position = newPos;
	}
}
