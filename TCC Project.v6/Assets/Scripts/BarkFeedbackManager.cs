using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarkFeedbackManager : MonoBehaviour {



	//Cria Barks após certa ação;
	public BarkManager barkTarget;
	public RemoteDialog remoteD; 
	public Dialogue barkSuccess;
	public Dialogue barkFail;
	public DialogueToFro diagFeedback;
	public GameObject dialogTarget;
	private bool condicaoLiberada = false;
	public Quest quest;
	public GameObject questHolder;
	public Text questText;
	public GlobalVariablesCaverna globVars;

	public void FeedbackBark(bool condicao){
		//triggando um bark em caso de sucesso ou falha de determinada ação
		if(condicao){
			barkTarget.RemoteBark(barkSuccess);
			remoteD.RemoteDiag(diagFeedback, dialogTarget);
			TrigaQuest();
		}else if(!condicaoLiberada){
			barkTarget.RemoteBark(barkFail);
		}
		condicaoLiberada = condicao;
	}


	void TrigaQuest(){
		//triggando uma quest apos determinado resultado da ação
		if(quest.prioridade >= globVars.prioridadeQuestAtual){
			questHolder.SetActive(true);
			questText.text = quest.descricao;
		}

	}
}
