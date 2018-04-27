using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSwitch : MonoBehaviour {

    public GameObject charJohanna;
    public GameObject charTommy;
    CameraController camManager;
	EnvCharChange[] envChangers;
	public GameObject world;
<<<<<<< HEAD
    public GameObject particlesTommy;
=======
>>>>>>> 402ad341a05c3d1af1d977ec893a5661e25d9122
    // Use this for initialization
    void Start () {
        camManager = CameraController.singleton;
		envChangers = world.GetComponentsInChildren<EnvCharChange> ();
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
<<<<<<< HEAD
                particlesTommy.SetActive(true);
				foreach (EnvCharChange changer in envChangers) {
					changer.Tommy ();
=======

				foreach (EnvCharChange changer in envChangers) {
					changer.Tommy ();
					print ("PIMBA");
>>>>>>> 402ad341a05c3d1af1d977ec893a5661e25d9122
				}
            }
            else
            {
                charJohanna.GetComponent<PlayerController>().myController = PlayerController.Controller.Active;
                charTommy.GetComponent<PlayerController>().myController = PlayerController.Controller.Inactive;
                camManager.target = charJohanna.transform;
<<<<<<< HEAD
                particlesTommy.SetActive(false);
=======

>>>>>>> 402ad341a05c3d1af1d977ec893a5661e25d9122
				foreach (EnvCharChange changer in envChangers)
					changer.Johanna ();
            }
        }
	}
}
