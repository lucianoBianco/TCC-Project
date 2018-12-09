using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]

public class Dialogue {

	// objecto de criação de diálogos
	
	[TextArea(3,10)]
	public string[] sentences;
	public bool hasCondition;
	public bool isFirstTime = true;
	public GameObject speakerImage;
}
