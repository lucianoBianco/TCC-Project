using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.AI;

public class ComplexDialogueManager : MonoBehaviour {


//manager mais complexo de trigger de dialogo
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
	public bool AsobrabTriga = false;
	public bool deactivate = false;
	private CameraController camCom;
	public Quest quest;
	
	public Animator[] anims;
	[Header("Caso Asobrab Triggue")]
	public GameObject Prayer;
	private int imagemIndex = 0;
	private GameObject oldPlayer = null;
	public GameObject questHolder;
	public Text questText;
	
	void Start () {
		sentences = new Queue<string> ();
		globalVarsCav = FindObjectOfType<GlobalVariablesCaverna>();
		camCom = (CameraController)FindObjectOfType(typeof(CameraController));
	}
	
	void OnTriggerEnter(Collider other){


		//trigado apenas pelo jogados ou apenas pelo npc definido aqui
		if(!deactivate){
			if(AsobrabTriga && other.transform.tag == "Asobrab"){
				if(dialogue.isFirstTime){
					mPlayer = Prayer;
					mPlayerMotor =  mPlayer.GetComponent<PlayerMotor>();
					mPlayerMotor.movBlocked = true;
					if(oldPlayer == null || oldPlayer.name != mPlayer.name){
						StartDialogue();
					}
					oldPlayer = mPlayer;
				}
			}
			else if (!AsobrabTriga && other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active) {
				if(dialogue.isFirstTime){
					mPlayerMotor =  other.gameObject.GetComponent<PlayerMotor>();
					mPlayer = other.gameObject;
					mPlayerMotor.movBlocked = true;
					if(oldPlayer == null || oldPlayer.name != mPlayer.name){
						StartDialogue();
					}
					oldPlayer = mPlayer;
				}
				
			}
		}
	}


	
	private void StartDialogue(){
		//inicia o dialogo
		if(!deactivate){
			if(!dialogueActive){
				camCom.SetDialoguetarget(null, true);
				globalVarsCav.TravarBarks(false);
				sentences.Clear();
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
		//itera pelas falas do diálogo
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
		//digita letra por letra por frame
		text.text = "";
		imagemIndex++;

		foreach (char letter in sentence.ToCharArray()) {
			text.text += letter;
			yield return null;
		}
	}
	
	public void FinishDialogue(){
		// encerra o diálogo e trigga barks, quests e destrava a camera e o jogador
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
			mPlayerMotor.movBlocked = false;
			dialogue.isFirstTime = false;
			TriggerAsobrab(ativarAsobrarb);
			TriggerSkill();
			if(dialogue.trigaQuest){
				TrigaQuest();
			}
		}
    }
	
	void Update(){
		//input para chamar a proxima frase do dialogo
		if (!GlobalVariablesCaverna.isPaused) {
			if (dialogueActive) {
				if (Input.GetButtonDown ("Ação1")) {
					DisplayNextSentence ();
				}
			}
		}
	}
	
	void TriggerAsobrab(bool move){
		//caso o dialogo deva trigar uma movimentação do npc
		if(move)
			asobrab.GetComponent<NavMeshAgent>().destination = target.position;
	}
	
	void TriggerSkill(){
		// caso o diálogo deva executar um poder de skill no final
		if(mPlayer != null && triggarSkill)
			mPlayer.GetComponent<JohannaSkills>().Activate();
		
	}


	void TrigaQuest(){
		// caso o diálogo triggue uma quest
		if(quest.prioridade > globalVarsCav.prioridadeQuestAtual){
			questHolder.SetActive(true);
			questText.text = quest.descricao;
		}
	}
}
