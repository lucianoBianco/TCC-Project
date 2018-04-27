using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogManager : MonoBehaviour {

	public TextMeshPro text;
	public Queue<string> sentences;
	public bool dialogueActive;

	public Dialogue[] dialogue;
	public bool finished = false;

	public Animator[] anims;

	// Use this for initialization
	void Start () {
		sentences = new Queue<string> ();
	}
	
	public void StartDialogue(int i){
		sentences.Clear ();
		dialogueActive = true;
		foreach (Animator item in anims) {
			item.SetBool ("IsOpen", true); 
		}
		foreach (string sentence in dialogue[i].sentences) {
			sentences.Enqueue (sentence);

		}
		DisplayNextSentence ();
		Debug.Log ("Foi");
	}

	public void DisplayNextSentence(){
		if (sentences.Count == 0) {
			FinishDialogue ();
			return;
			dialogueActive = false;
		}

		string sentence = sentences.Dequeue ();
		StopAllCoroutines ();
		StartCoroutine (TypeSentence (sentence));
	}

	IEnumerator TypeSentence(string sentence){
		text.text = "";

		foreach (char letter in sentence.ToCharArray()) {
			text.text += letter;
			yield return null;
		}
	}

	public void EndDialogue(){
		Debug.Log ("Fim de transmissão");
		foreach (Animator item in anims) {
			item.SetBool ("IsOpen", false); 
		}

	}

	public void FinishDialogue(){
		finished = true;
		foreach (Animator item in anims) {
			item.SetBool ("IsOpen", false); 
		}
		text.text = "";
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
}
