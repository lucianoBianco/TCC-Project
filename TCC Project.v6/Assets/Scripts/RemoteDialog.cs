using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class RemoteDialog : MonoBehaviour {

	//permite a ativação de diálogos por triggers externos como o término de um puzzle ou do board musical ao final

	public Text text;
	public Queue<string> sentences;
	public bool dialogueActive;

	public DialogueToFro dialogue;
	public bool finished = false;
	private PlayerMotor mPlayerMotor;
	private GameObject mPlayer;
	public String anim;
	public Transform target;
	public bool ativarAsobrarb;
	public GameObject asobrab;
	private GlobalVariablesCaverna globalVarsCav;
	public bool triggarSkill = false;
	public bool deactivate = false;
	private CameraController camCom;
	public GameObject dialogueTarget;
	
	public Animator[] anims;
	private int imagemIndex = 0;
	
	void Start () {
		sentences = new Queue<string> ();
		globalVarsCav = FindObjectOfType<GlobalVariablesCaverna>();
		camCom = (CameraController)FindObjectOfType(typeof(CameraController));
		dialogueTarget = null;
	}

// chama o diálogo de uma outra fonte
	public void RemoteDiag(DialogueToFro diag, GameObject diagTarget){
		dialogue = diag;
		dialogueTarget = diagTarget;
		StartDialogue();
	}

//funciona como o ComplexDialogueManager
	private void StartDialogue(){
		if(!deactivate){
			camCom.SetDialoguetarget(dialogueTarget, true);
			globalVarsCav.TravarBarks(false);
			sentences.Clear();
			dialogueActive = true;
			foreach (Animator item in anims) {
				item.SetBool ("IsOpen", true);
			}
			foreach (string sentence in dialogue.sentences) {
				sentences.Enqueue (sentence);

			}
			foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
                if (player.GetComponent<PlayerMotor>() != null)
                    player.GetComponent<PlayerMotor>().movBlocked = true;
			}
			DisplayNextSentence ();
		}
	}
	
	public void DisplayNextSentence(){
		if(!deactivate){
			if (sentences.Count == 0) {
				FinishDialogue ();
				dialogueActive = false;
				return;
			}
			try{
				dialogue.speakerImage[imagemIndex - 1].SetActive(false);
			}catch(Exception e){
				//array out of bounds
			}
			dialogue.speakerImage[imagemIndex].SetActive(true);

			string sentence = sentences.Dequeue ();
			StopAllCoroutines ();
			StartCoroutine (TypeSentence (sentence));
		}
	}
	
	IEnumerator TypeSentence(string sentence){
		text.text = "";
		imagemIndex++;

		foreach (char letter in sentence.ToCharArray()) {
			text.text += letter;
			yield return null;
		}
	}
	
	public void FinishDialogue(){
		if(!deactivate){
			camCom.SetDialoguetarget(null, false);
			globalVarsCav.TravarBarks(true);
			foreach (Animator item in anims) {
				item.SetBool ("IsOpen", false); 
			}
			try{
				dialogue.speakerImage[imagemIndex - 1].SetActive(false);
			}catch(Exception e){
				Debug.LogException(e);
				//array out of bounds
			}
			text.text = "";
			StopAllCoroutines ();
			foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
                if (player.GetComponent<PlayerMotor>() != null)
                    player.GetComponent<PlayerMotor>().movBlocked = false;
			}
			dialogue.isFirstTime = false;
			TriggerAsobrab(ativarAsobrarb);
			TriggerSkill();
		}
    }
	
	void Update(){
		if (!GlobalVariablesCaverna.isPaused) {
			if (dialogueActive) {
				if (Input.GetButtonDown ("Ação1")) {
					DisplayNextSentence ();
				}
			}
		}
	}
	
	void TriggerAsobrab(bool move){
		if(move)
			asobrab.GetComponent<NavMeshAgent>().destination = target.position;
	}
	
	void TriggerSkill(){
		if(mPlayer != null && triggarSkill)
			mPlayer.GetComponent<JohannaSkills>().Activate();
		
	}
}
