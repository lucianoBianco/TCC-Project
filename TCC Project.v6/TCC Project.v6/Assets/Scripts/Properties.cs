using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour {
	GlobalVariables vars;
	public GameObject varsObj;

	public bool active = false;


	// Use this for initialization
	void Start () {
		vars = varsObj.GetComponent<GlobalVariables> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void Action(bool b)
    {
		if (b) {
			vars.Interact (this.gameObject);
		}
    }
}
