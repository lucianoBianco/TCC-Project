using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchFocus : MonoBehaviour {

	// gerencia os objetos ao redor do personagem tommy que estão no alcance de serem travados pela mira

	public static List<GameObject> listaFocusObjs = new List<GameObject>();
	private static int index;
	

// quando o objeto entra na proximidade necessária ele é adicionado a uma lista e é removido caso o jogador se afaste do objeto
	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "FocusObj"){
			listaFocusObjs.Add(other.gameObject);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.transform.tag == "FocusObj"){
			listaFocusObjs.Remove(other.gameObject);
		}
	}
	
	// itera entre os alvos disponiveis na lista
	public static GameObject TrocaAlvo(){
		int targetIndex = index;
		
		if (index < listaFocusObjs.Count){
			index++;
		}
		else{
			index = 0;
			targetIndex = 0;
			return null;
		}
		return listaFocusObjs[targetIndex];
	}
	

	// remotamente remove ou adiciona um alvo a lista
	public void RemoveFocus(GameObject go){
		listaFocusObjs.Remove(go);
	}

	public void AddFocus(GameObject go){
		listaFocusObjs.Add(go);
	}
}
