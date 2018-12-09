using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class Quest {

//objeto que contem as informações de cada quest triggada

	[TextArea(3,10)]
	public string descricao;
	public int number;
	public int prioridade;

}
