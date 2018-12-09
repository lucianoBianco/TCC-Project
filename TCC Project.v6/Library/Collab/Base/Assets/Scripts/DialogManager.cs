using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

	public Text text;
	public Queue<string> sentences;
	public bool dialogueActive;

	public Dialogue[] dialogue;
	public bool finished = false;
	int index = 0;
    private PlayerMotor mPlayer;

	public Animator[] anims;
	public bool deactivate = false;
	private CameraController camCom;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string> ();
		camCom = (CameraController)FindObjectOfType(typeof(CameraController));
	}
	
	public void StartDialogue(int i, Transform player){
		if(!deactivate){
			camCom.SetDialoguetarget(this.gameObject, true);
			dialogue[i].speakerImage.enabled = true;
			sentences.Clear ();
			dialogueActive = true;
			foreach (Animator item in anims) {
				item.SetBool ("IsOpen", true); 
			}
			foreach (string sentence in dialogue[i].sentences) {
				sentences.Enqueue (sentence);
				index = i;

			}
			DisplayNextSentence ();
			mPlayer = player.GetComponent<PlayerMotor>();
			mPlayer.movBlocked = true;
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
			dialogue[index].speakerImage.enabled = false;
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
		transform.LookAt (Camera.main.transform);
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
}
