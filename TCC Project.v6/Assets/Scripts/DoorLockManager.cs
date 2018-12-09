using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLockManager : MonoBehaviour {

	//controlador obsoleto da porta placeholder ao final do level
	public Animator door1, door2;
	public GameObject doorLock;

	public void OpenDoors(){
		door1.SetBool ("Open", true);
		door2.SetBool ("Open", true);
		doorLock.SetActive (false);
	}
}
