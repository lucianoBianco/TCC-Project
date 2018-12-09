using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TommyBoom : MonoBehaviour {

	// controla os objetos que o tommy pode explodir pelas skills

	private bool condition1, puzzleLock;
	public Checkpoint check;
	public Animator[] anims;
	public string pedraName;
	public TommyTarget puzzle1, puzzle2;


// ativa as animações pertinentes caso a sequencia de ativação do objeto esteja correta
	public void Boom(GameObject go){
		if(!puzzleLock){
			if(!condition1){
				if(go.name == pedraName){
					condition1 = true;
					go.GetComponent<TommyTarget>().Succeed();
					go.GetComponent<TommyTarget>().BarkFeedback();
				}else{
					go.GetComponent<TommyTarget>().Fail();
					go.GetComponent<TommyTarget>().BarkFeedback();
					FailPuzzle(go);
				}
			}else{
				go.GetComponent<TommyTarget>().Succeed();
				check.SaveCheck();
			}
		}
	}

// caso algum objeto deveria ter sido destruido antes desse ele ativa a animação de falha
	private void FailPuzzle(GameObject go){
		puzzleLock = true;
		float timer = 5f;
		StartCoroutine(Fog(timer, go));
	}

// fog de reset caso o puzzle de destruição tenha sido feito da maneira a travar a solução
	IEnumerator Fog(float timer, GameObject go){
		while (timer > 0){
			timer-= Time.deltaTime;
			yield return null;
		}
		go.GetComponent<TommyTarget>().ResetPuzzle();
		while(RenderSettings.fogDensity < 1){
			RenderSettings.fogDensity += 0.5f * Time.deltaTime;
			yield return null;
		}
		foreach(Animator anim in anims){
			anim.SetTrigger("reset");
			Debug.Log("twice");
		}
		Debug.Log("justonce");
		while (RenderSettings.fogDensity > 0){
			RenderSettings.fogDensity -= 0.5f * Time.deltaTime;
			yield return null;
		}
		puzzleLock = false;

	}

// triggado ao fazer um load num ponto além do puzzle
	public void CompletePuzzle(){
		puzzle1.Complete();
		puzzle2.Complete();
	}
}
