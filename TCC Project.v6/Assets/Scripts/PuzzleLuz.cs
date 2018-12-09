using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PuzzleLuz : MonoBehaviour {

    //Controlador de puzzle de luz
    public List<GameObject> puzzleTiles;
    public float intensidade, range;
    public Animator anim;
    public ShaderPuzzleComplete shaderTrigger;


    [Header ("Barks Feedback")]
    public Dialogue[] feedbacks;
    public GameObject target;


    public bool lockPuzzle = false;
    private GameObject johanna;
    private bool[] listaPuzzleactive;
	private bool travaBarkInicial = true;
    private FeedbackDialoguePuzzle FDP;
    public bool salvar = false;



	void Awake () {

		if(anim == null){
			anim = gameObject.GetComponent<Animator>();
        }
		listaPuzzleactive = new bool[puzzleTiles.Count];
        for (int i = 0; i < puzzleTiles.Count; i++)
        {
            puzzleTiles[i].GetComponent<PuzzleTile>().parent = this;
        }

        FDP = GetComponent<FeedbackDialoguePuzzle>();
	}
	
	void Update () {
        if (!lockPuzzle)
        {
            //verifica se o valor de intensidade da luz dos tiles é suficiente para manter o puzzle ativo
            if (range < 1f){
                ResetPuzzle();
				if(!travaBarkInicial)
					TriggerBark(2);	
			}

            for (int i = 0; i < puzzleTiles.Count; i++)
            {
                bool estado = listaPuzzleactive[i];

                //define, para cada tile do puzzle, se deve estar ligado ou não
                if (estado)
                {
					travaBarkInicial = false;
                    puzzleTiles[i].GetComponentInChildren<Light>().range = range - 10;
                    puzzleTiles[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * intensidade * 7);
                }
                else
                {

                    puzzleTiles[i].GetComponentInChildren<Light>().range = 0;
                    puzzleTiles[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * 0);
                }
            }

        }

    }


    public void AtualizarEstado(GameObject go)
    {
        // ao pisar em um tile, verifica se o mesmo já se encontra aivado, caso positivo o puzzle é resetado, caso contrário o tile é ativado
        if (listaPuzzleactive[puzzleTiles.IndexOf(go)])
        {
            ResetPuzzle();
			if(!lockPuzzle){
				TriggerBark(1);
			}
        }
        else
        {
            listaPuzzleactive[puzzleTiles.IndexOf(go)] = true;
            puzzleTiles[puzzleTiles.IndexOf(go)].GetComponent<PuzzleTile>().isAtivado = true;
            TriggerBark(0);
        }
        CheckCondition();
    }


    public void ResetPuzzle()
    {
        // apaga todos os tiles do puzzle
        for (int i = 0; i < listaPuzzleactive.Length; i++)
        {
            listaPuzzleactive[i] = false;
            puzzleTiles[i].GetComponent<PuzzleTile>().isAtivado = false;
        }
        range = 0;
        intensidade = 0;
    }

    public void CheckCondition()
    {
        // verifica se todos os tiles ja foram acesos
        for (int i = 0; i < listaPuzzleactive.Length; i++)
        {
            if (!listaPuzzleactive[i])
                return;
        }
        salvar = true;
        CompletePuzzle();
    }

    public void CompletePuzzle()
    {
        //ativa aas animações e feedbacks caso o puzzle tenha sido concluido
		if(anim != null){
			anim.SetBool("Complete", true);
        }
		lockPuzzle = true;
        shaderTrigger.Complete();
        LockColorMaterial();
		DestraravarMovJohanna();
    }

    private void LockColorMaterial()
    {
        // trava os valores de luz após o puzzle ter sido concluido
        for (int i = 0; i < puzzleTiles.Count; i++)
        {
            puzzleTiles[i].GetComponentInChildren<Light>().range = 5;
            puzzleTiles[i].GetComponent<Renderer>().material.SetColor("_EmissionColor", new Color(1f, 1f, 1f) * 5f);
        }
    }

    // trava a movimentação da IA da johanna caso esteja durante o progresso de um puzzle
	void OnTriggerEnter(Collider other){
		if (other.gameObject.GetComponent<JohannaSkills> () != null){
			johanna = other.gameObject;
			TravarMovJohanna();
		}
	}
	
	void OnTriggerExit(Collider other){
		if (other.gameObject.GetComponent<JohannaSkills> () != null){
			johanna = other.gameObject;
			DestraravarMovJohanna();
		}
	}
	
	void TravarMovJohanna (){
        if(!lockPuzzle){
            johanna.GetComponent<PlayerMotor>().puzzleBlocked = true;
        }
	}
	
	void DestraravarMovJohanna (){
        if(johanna != null)
            johanna.GetComponent<PlayerMotor>().puzzleBlocked = false;
	}

// trigga um bark de resposta ao resultado do puzzle
    void TriggerBark(int caso){
		if(feedbacks.Length > 0){
            if(feedbacks[caso].isFirstTime){
                FDP.FeedbackDialogue(feedbacks[caso]);
                feedbacks[caso].isFirstTime = false;
            }
		}
    }
}
