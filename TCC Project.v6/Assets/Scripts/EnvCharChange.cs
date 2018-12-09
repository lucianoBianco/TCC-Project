using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnvCharChange : MonoBehaviour {

// script de mudança de material ao trocar de personagem
	public Material[] TreeColorMat;
	Material[] CurrMats;
	Renderer rend;
	public int matIndex;
	void Start () {
		rend = GetComponent<Renderer> ();
	}

	
//métodos para troca de texturas referente a cada um dos 4 personagens
	public void Johanna(){
		try{
		if (rend != null && TreeColorMat[0] != null) {
			CurrMats = rend.materials;
			CurrMats [matIndex] = TreeColorMat [0];
			rend.materials = CurrMats;
		}
		}
		catch(Exception e){
		return;
		}
	}
	public void Tommy(){
		try{
		if (rend != null && TreeColorMat[1] != null) {
			CurrMats = rend.materials;
			CurrMats [matIndex] = TreeColorMat [1];
			rend.materials = CurrMats;
		}
		}
		catch(Exception e){
		return;
		}
	}
	public void Bia(){
		if (rend != null) {
			CurrMats = rend.materials;
			CurrMats [matIndex] = TreeColorMat [2];
			rend.materials = CurrMats;
		}
	}
	public void Yuki(){
		if (rend != null) {
			CurrMats = rend.materials;
			CurrMats [matIndex] = TreeColorMat [3];
			rend.materials = CurrMats;
		}
	}
}
