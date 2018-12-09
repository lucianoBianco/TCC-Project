using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarkTrigger : MonoBehaviour {


//Trigga bark por collisão
	public GameObject trigger;
	private BarkManager manager;
	public GameObject target;
	public Dialogue bark;
	public BarkTrigger conditionTarget;
	public bool condition = true;


	void OnTriggerStay(Collider other){
		if(other.gameObject == trigger){
			if(bark.isFirstTime){
				manager =  target.GetComponentInChildren<BarkManager>();
				if(!manager.barking && condition){
					if(conditionTarget != null)
						conditionTarget.condition = true;
					bark.isFirstTime = false;
					manager.RemoteBark(bark);
				}
			}
		}
	}
}
