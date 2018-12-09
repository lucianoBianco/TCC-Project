
using UnityEngine;

public class Interactibles : MonoBehaviour {

	//classe que controla a interação para ativação do board

	public float radius = 3f;
	public GameObject hud;
	private bool isOn;
	private GameObject colisor;
	public bool boardIsOn;
	private bool outofrange = false;

	void OnDrawGizmosSelected(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
//ativa o 'press E to interact' na hud
	void OnTriggerStay(Collider other){
		if(other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active){
			isOn = true;
			colisor = other.gameObject;
			outofrange = false;
		}
	}
// mostra ou esconde a hud
	void Update(){
		if(!boardIsOn){
			if(isOn){
				isOn = false;
				hud.SetActive(true);
				colisor.GetComponent<PlayerController> ().props = GetComponent<Properties> ();
			}else{
				Deactvate();
			}
		}else{
			Deactvate();
		}
	}
// esconde a hud
	void Deactvate(){
		if(!outofrange){
			hud.SetActive(false);
			if(colisor!= null)
				colisor.GetComponent<PlayerController> ().props = null;
			outofrange = true;
		}
	}
}
