using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogCondition : MonoBehaviour {
	public int dialogueIndex;
	public DialogManager Dialogue;


	public void LiberarCondition(){
		// caso o diálogo deva ser impedido de prosseguir caso algo deva ser feito pelo jogador antes
		Dialogue.LiberarCondition (dialogueIndex);
	}
}
