using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderPuzzleComplete : MonoBehaviour {

	// controla o shader do 'tile safe' no centro dos puzzles
	private bool ativator = false;
	public Renderer rend;
	private float var = 0;
	
	public void Complete(){
		ativator = true;
	}

// gradualmente altera o valor dentro do shader para causar o efeito
	void Update(){
		if(ativator){
			rend.material.SetFloat("_Activation", var);
			var += Time.deltaTime * 0.1f;
		}
	}
}
