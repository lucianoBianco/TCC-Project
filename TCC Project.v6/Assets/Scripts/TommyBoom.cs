using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyBoom : MonoBehaviour {

	public Transform brokenObj;


	public void Boom(){
		Destroy (gameObject);
		Instantiate(brokenObj,transform.position, transform.rotation);
	}
}
