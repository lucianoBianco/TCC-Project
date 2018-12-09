using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.AI;

public class DialogueManagerToFro : MonoBehaviour {
	public Text text;
	public Queue<string> sentences;
	public bool dialogueActive;
//gerenciador de diálogos de mais de um locutor
	//semelhante ao complex dialogue manager
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
	public bool AsobrabTriga = false;
	public bool deactivate = false;
	private CameraController camCom;
	private int imagemIndex = 0;
	private GameObject oldPlayer = null;
	private bool trigado = false;
	
	public Animator[] anims;
	
	
	void Start () {
		sentences = new Queue<string> ();
		globalVarsCav = FindObjectOfType<GlobalVariablesCaverna>();
		camCom = (CameraController)FindObjectOfType(typeof(CameraController));
	}
	
	void OnTriggerEnter(Collider other){
		if(!deactivate){
			if(!trigado && AsobrabTriga && other.transform.tag == "Asobrab"){
				trigado = true;
				if(dialogue.isFirstTime){
					foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
						player.GetComponent<PlayerMotor>().movBlocked = true;
					}
					StartDialogue();
				}
			}
			else if (!AsobrabTriga && other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active) {
				if(dialogue.isFirstTime){
					mPlayerMotor =  other.gameObject.GetComponent<PlayerMotor>();
					mPlayer = other.gameObject;
					mPlayerMotor.movBlocked = true;
					if(oldPlayer != null && oldPlayer.name == mPlayer.name){
						StartDialogue();
					}
					oldPlayer = mPlayer;
				}
			}
		}
	}
	
	private void StartDialogue(){
		if(!deactivate){
			if(!dialogueActive){
				camCom.SetDialoguetarget(target.gameObject, true);
				globalVarsCav.TravarBarks(false);
				globalVarsCav.gameObject.GetComponent<CharacterSwitch>().LiberarChar = false;
				sentences.Clear ();
				dialogueActive = true;
				foreach (Animator item in anims) {
					item.SetBool ("IsOpen", true); 
				}
				foreach (string sentence in dialogue.sentences) {
					sentences.Enqueue (sentence);
				}
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
			try{
				dialogue.speakerImage[imagemIndex - 1].SetActive(false);
			}catch(Exception e){
				Debug.LogException(e);
				//array out of bounds
			}
			globalVarsCav.gameObject.GetComponent<CharacterSwitch>().LiberarChar = true;
			globalVarsCav.TravarBarks(true);
			foreach (Animator item in anims) {
				item.SetBool ("IsOpen", false); 
			}
			text.text = "";
			StopAllCoroutines ();
			foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
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
