using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BarkManager : MonoBehaviour {



    //Gerencia os barks dos personagens
    float timmer = 0f;
    public Dialogue[] barks;
    public Text text;
    public Animator anim;
	public bool barking = false;
	public bool barkingAllowed = true;
	void Start () {
        timmer = 20f;
    }
	
	void Update () {
        //passando o tempo de espera até executar o proximo bark
        timmer -= Time.deltaTime;

        if(timmer <= 0)
        {
            ResetTimmer();
            Bark();
        }
    }

    public void Bark()
    {
        //executar o bark
        if(barks.Length > 0 && barkingAllowed){
            int barkIndex = Random.Range(0, barks.Length);
            StartCoroutine(DisplaySentence(barks[barkIndex].sentences[0]));
        }
    }

    public void ResetTimmer()
    {
        //define um tempo aleatório até o proximo bark
        timmer = Random.Range(20f, 50f) + 5f;
    }

    IEnumerator DisplaySentence(string sentence)
    {
        //trigga um bark que dura um certo tempo
		if(!barking){
        text.text = sentence;
        anim.SetBool("Display", true);
		barking = true;
        yield return new WaitForSeconds(10);
        anim.SetBool("Display", false);
		barking = false;
		}
	}
	
	public void RemoteBark(Dialogue bark){
        //possibilita a criação de barks atravez de outras classes
		StartCoroutine(DisplaySentence(bark.sentences[0]));
	}
}
