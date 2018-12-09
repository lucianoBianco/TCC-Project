using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckConditionPuzzle : MonoBehaviour {

	//verifica se o puzzle foi concluido com sucesso e salva o progresso
	private PuzzleLuz puzzle;
	private bool puzzleOver = false;
	private Checkpoint checkpoint;

	void Start(){
		puzzle = GetComponent<PuzzleLuz>();
		checkpoint = GetComponent<Checkpoint>();
	}

	void Update(){
		if(!puzzleOver){
			if(puzzle.salvar){
				checkpoint.SaveCheck();
				puzzleOver = true;
			}
		}
	}
}
