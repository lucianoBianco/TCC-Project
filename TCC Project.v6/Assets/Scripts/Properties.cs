using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Properties : MonoBehaviour {
	GlobalVariablesCaverna vars;
	public GameObject varsObj;

	public bool active = false;

//atributo de interação de objetos

	void Start () {
		vars = varsObj.GetComponent<GlobalVariablesCaverna> ();
	}
	void Update () {
		
	}
    public void Action(bool b)
    {
		if (b) {
			vars.Interact (this.gameObject);
		}
    }
}
