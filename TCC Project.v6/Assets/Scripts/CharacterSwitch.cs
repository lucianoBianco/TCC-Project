using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterSwitch : MonoBehaviour {

	//classe que controla os estados dos personagens jogaveis na troca de controles pelo 'tab'
    public GameObject charJohanna;
	public GameObject charTommy;
    CameraController camManager;
	public static EnvCharChange[] envChangers;
	public GameObject world;
    public GameObject particlesTommy;
	public bool LiberarChar = false;
	public GameObject particlesJohanna;

	public Animator anim;

    void Start () {
        camManager = CameraController.singleton;
		envChangers = GameObject.FindObjectsOfType<EnvCharChange>();
    }
	

	void Update () {
		if(LiberarChar){
			if (Input.GetButtonDown ("Ação2")) {
				anim.SetTrigger("Switch");
				if (charJohanna.GetComponent<PlayerController> ().myController == PlayerController.Controller.Active) {
					charJohanna.GetComponent<PlayerController> ().myController = PlayerController.Controller.Inactive;
					charJohanna.GetComponent<PlayerController> ().ResetLockon();
					charTommy.GetComponent<PlayerController> ().ResetLockon();
					charJohanna.GetComponent<NavMeshAgent>().enabled = true;
					if(charTommy.GetComponent<NavMeshAgent>().isActiveAndEnabled)
						charTommy.GetComponent<NavMeshAgent>().ResetPath ();
					charTommy.GetComponent<NavMeshAgent>().enabled = false;
					charTommy.GetComponent<PlayerController> ().myController = PlayerController.Controller.Active;
					charTommy.GetComponentInChildren<Animator>().SetFloat ("Speed", 0);
					charJohanna.GetComponent<PlayerMotor>().playerAtual = charTommy;
					camManager.target = charTommy.transform;
					particlesTommy.SetActive (true);
					particlesJohanna.SetActive(false);
					foreach (EnvCharChange changer in envChangers) {
						changer.Tommy ();
					}
				} else if (charTommy.GetComponent<PlayerController> ().myController == PlayerController.Controller.Active) {
					charJohanna.GetComponent<PlayerController> ().myController = PlayerController.Controller.Active;
					charJohanna.GetComponent<NavMeshAgent>().enabled = false;
					charJohanna.GetComponent<PlayerController> ().ResetLockon();
					charTommy.GetComponent<PlayerController> ().ResetLockon();
					if (charJohanna.GetComponent<NavMeshAgent>().isActiveAndEnabled)
						charJohanna.GetComponent<NavMeshAgent>().ResetPath ();
					charTommy.GetComponent<NavMeshAgent>().enabled = true;
					charTommy.GetComponent<PlayerController> ().myController = PlayerController.Controller.Inactive;
					charTommy.GetComponent<PlayerMotor>().playerAtual = charJohanna;
					camManager.target = charJohanna.transform;
					particlesTommy.SetActive (false);
					particlesJohanna.SetActive(true);
					foreach (EnvCharChange changer in envChangers)
						changer.Johanna();
				}
			}
		}
	}


//libera o personagem tommy para ser selecionavel apos sair da caverna
	public void LiberarTommy(){
		LiberarChar = true;
		charTommy.GetComponent<PlayerController> ().ResetLockon();
		if (charJohanna.GetComponent<NavMeshAgent>().isActiveAndEnabled)
			charJohanna.GetComponent<NavMeshAgent>().ResetPath ();
		charTommy.GetComponent<NavMeshAgent>().enabled = true;
        charTommy.GetComponentInChildren<Animator>().SetBool("OnGround", true);
		charTommy.GetComponent<PlayerController> ().myController = PlayerController.Controller.Inactive;
		charTommy.GetComponent<PlayerMotor>().playerAtual = charJohanna;
	}


}
