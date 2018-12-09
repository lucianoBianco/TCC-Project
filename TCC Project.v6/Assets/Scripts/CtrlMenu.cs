using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CtrlMenu : MonoBehaviour {

	//controle da musica e da camera do menu inicial
	public CinemachineVirtualCamera Cmvc;
	public AudioClip transition1, transition2, loop1, loop2;
	public AudioSource audioSource;
	private bool menuOn, jaTransito;
	public GameObject pressKey;
	


	void Start(){
		
		
		
		
	}

	void Update () {
		if(!audioSource.isPlaying && !menuOn)
        {
            audioSource.clip = loop1;
            audioSource.Play();
        }
		
		else if(!audioSource.isPlaying && menuOn&& !jaTransito)
		{
			audioSource.clip = transition2;
			audioSource.Play();
			jaTransito=true;
		}
		else if (!audioSource.isPlaying && menuOn && jaTransito)
		{
			
			audioSource.clip = loop2;
			audioSource.Play();
			
		}

		 if (Input.anyKey)
        {
        	pressKey.SetActive(false);
			menuOn=true;
            Cmvc.Priority=11;
        }
	}
}
