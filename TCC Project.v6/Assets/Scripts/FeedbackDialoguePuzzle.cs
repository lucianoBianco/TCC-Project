using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FeedbackDialoguePuzzle : MonoBehaviour {

	public Text text;
	public Queue<string> sentences;
	public bool dialogueActive;
//diálogo triggado durante um puzzle para tutorial de falhas
	private Dialogue dialogue;
	public bool finished = false;
	private PlayerMotor mPlayerMotor;
	public String anim;
	public Transform target;
	private GlobalVariablesCaverna globalVarsCav;
	public bool triggarSkill = false;
	public bool deactivate = false;
	private CameraController camCom;
	
	public Animator[] anims;
	
	
	void Start () {
		sentences = new Queue<string> ();
		globalVarsCav = FindObjectOfType<GlobalVariablesCaverna>();
		camCom = (CameraController)FindObjectOfType(typeof(CameraController));
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active) {
			mPlayerMotor =  other.gameObject.GetComponent<PlayerMotor>();
		}
	}
	
	public void FeedbackDialogue(Dialogue diag){
		if(!deactivate){
			if(mPlayerMotor!= null)
				mPlayerMotor.movBlocked = true;
			dialogue = diag;
			StartDialogue();
		}
	}
	
	private void StartDialogue(){
		if(!deactivate){
			camCom.SetDialoguetarget(null, true);
			globalVarsCav.TravarBarks(false);
			sentences.Clear ();
			dialogueActive = true;
			foreach (Animator item in anims) {
				item.SetBool ("IsOpen", true); 
			}
			foreach (string sentence in dialogue.sentences) {
				sentences.Enqueue (sentence);
			}
			DisplayNextSentence ();
			if(dialogue.speakerImage != null)
				dialogue.speakerImage.SetActive(true);

			
		}
	}
	
	public void DisplayNextSentence(){
		if(!deactivate){
			if (sentences.Count == 0) {
				FinishDialogue ();
				dialogueActive = false;
				return;
			}

			string sentence = sentences.Dequeue ();
			StopAllCoroutines ();
			StartCoroutine (TypeSentence (sentence));
		}
	}
	
	IEnumerator TypeSentence(string sentence){
		text.text = "";

		foreach (char letter in sentence.ToCharArray()) {
			text.text += letter;
			yield return null;
		}
	}
	
	public void FinishDialogue(){
		if(!deactivate){
			camCom.SetDialoguetarget(null, false);
			if(dialogue.speakerImage != null)
				dialogue.speakerImage.SetActive(false);
			globalVarsCav.TravarBarks(true);
			foreach (Animator item in anims) {
				item.SetBool ("IsOpen", false); 
			}
			text.text = "";
			StopAllCoroutines ();
			if(mPlayerMotor!= null)
				mPlayerMotor.movBlocked = false;
			dialogue.isFirstTime = false;
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
}

