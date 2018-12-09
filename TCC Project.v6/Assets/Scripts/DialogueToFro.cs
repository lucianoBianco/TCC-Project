using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class DialogueToFro {
// objecto de diálogo mais complexo com triggers extras
	[TextArea(3,10)]
	public string[] sentences;
	public bool hasCondition;
	public bool isFirstTime = true;
	public GameObject[] speakerImage;
	public bool trigaQuest;
	public bool trigaAudio;
	public AudioSource audio;

}
