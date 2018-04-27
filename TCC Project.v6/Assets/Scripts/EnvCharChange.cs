using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvCharChange : MonoBehaviour {


	public Material[] TreeColorMat;
	Material[] CurrMats;
	Renderer rend;
	public int matIndex;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Johanna(){
		CurrMats = rend.materials;
		CurrMats[matIndex] = TreeColorMat [0];
		rend.materials = CurrMats;
<<<<<<< HEAD
=======

>>>>>>> 402ad341a05c3d1af1d977ec893a5661e25d9122
	}
	public void Tommy(){
		CurrMats = rend.materials;
		CurrMats[matIndex] = TreeColorMat [1];
		rend.materials = CurrMats;
<<<<<<< HEAD
    }
=======
		print ("VAI");
	}
>>>>>>> 402ad341a05c3d1af1d977ec893a5661e25d9122
}
