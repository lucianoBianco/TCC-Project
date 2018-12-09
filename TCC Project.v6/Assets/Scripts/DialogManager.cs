using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DialogManager : MonoBehaviour {
// gerenciador de dialógo simples
	// funcionamento semelhante ao complex dialogue manager
	public Text text;
	public Queue<string> sentences;
	public bool dialogueActive;

	public DialogueToFro[] dialogue;
	public bool finished = false;
	int index = 0;
    private PlayerMotor mPlayer;
    public Quest[] quests;
	public Animator[] anims;
	public bool deactivate = false;
	private CameraController camCom;
	private bool sentenceFinished = true;
	private string sentence;
	private int imagemIndex = 0;
	public Text questText;
	public GameObject QuestHolder;

	void Start () {
		sentences = new Queue<string> ();
		camCom = (CameraController)FindObjectOfType(typeof(CameraController));
	}
	
	public void StartDialogue(int i, Transform player){
		if(!deactivate){
			if(!dialogueActive){
				imagemIndex = 0;
				camCom.SetDialoguetarget(this.gameObject, true, true);
				sentences.Clear ();
				dialogueActive = true;
				foreach (Animator item in anims) {
					item.SetBool ("IsOpen", true); 
				}
				foreach (string sentence in dialogue[i].sentences) {
					sentences.Enqueue (sentence);
					index = i;
				}
				mPlayer = player.GetComponent<PlayerMotor>();
				mPlayer.movBlocked = true;
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
				dialogue[index].speakerImage[imagemIndex - 1].SetActive(false);
			}catch(Exception e){
				//array out of bounds
			}
			dialogue[index].speakerImage[imagemIndex].SetActive(true);
			sentenceFinished = false;
			sentence = sentences.Dequeue ();
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
		sentenceFinished = true;
	}

	private void overrideSentece(){
		StopAllCoroutines();
		text.text = "";
		text.text = sentence;
		sentenceFinished = true;
	}

	public void FinishDialogue(){
		if(!deactivate){
			if(dialogue[index].trigaAudio)
				dialogue[index].audio.Play();
			camCom.SetDialoguetarget(null, false);
			try{
				dialogue[index].speakerImage[imagemIndex - 1].SetActive(false);
			}catch(Exception e){
				//Debug.LogException(e);
				//array out of bounds
			}
			if (dialogue [index].hasCondition) {
				finished = false;
			} else{
				finished = true;
				transform.GetComponentInParent<AsobrarbMovement>().balao.SetActive(false);
			}
			foreach (Animator item in anims) {
				item.SetBool ("IsOpen", false); 
			}
			text.text = "";
			StopAllCoroutines ();
			mPlayer.movBlocked = false;
			dialogue[index].isFirstTime = false;
			transform.GetComponentInParent<AsobrarbMovement>().UnlockDialog();
			if(dialogue[index].trigaQuest){
				TrigaQuest(index);
			}

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
		//transform.LookAt (Camera.main.transform);
	}

	public void LiberarCondition(int i){
		dialogue [i].hasCondition = false;
		//FinishDialogue ();
	}

	public void CheckFirst(int i, Transform player){
		if(dialogue[i].isFirstTime){
			StartDialogue(i, player);
		}
	}

	void TrigaQuest(int questNumber){

		foreach(Quest quest in quests){
			if(quest.number == questNumber){
				questText.text = quest.descricao;
				QuestHolder.SetActive(true);
				return;
			}
		}

	}
}
