using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour {

	public int points;
	public GlobalVariables vars;
	bool finish = false;

    void Start()
    {
        vars = GameObject.Find("World State").GetComponent<GlobalVariables>();
    }


    void Update(){
		if (!finish && points == 100) {
			vars.MusicOk ();
			finish = false;
		}
	}

	public void Destruir(){
		Destroy (gameObject);

	}
}
