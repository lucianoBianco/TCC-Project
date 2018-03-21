using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour {

    public GameObject charJohanna;
    public GameObject charTommy;
    CameraController camManager;
    // Use this for initialization
    void Start () {
        camManager = CameraController.singleton;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Ação2"))
        {
            if (charJohanna.GetComponent<PlayerController>().myController == PlayerController.Controller.Active)
            {
                charJohanna.GetComponent<PlayerController>().myController = PlayerController.Controller.Inactive;
                charTommy.GetComponent<PlayerController>().myController = PlayerController.Controller.Active;
                camManager.target = charTommy.transform;
            }
            else
            {
                charJohanna.GetComponent<PlayerController>().myController = PlayerController.Controller.Active;
                charTommy.GetComponent<PlayerController>().myController = PlayerController.Controller.Inactive;
                camManager.target = charJohanna.transform;
            }
        }
	}
}
