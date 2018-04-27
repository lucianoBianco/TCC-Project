using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyBoom : MonoBehaviour {

	public Transform brokenTree;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Boom(){
		Destroy (gameObject);
		Instantiate(brokenTree,transform.position, transform.rotation);
	}
}
