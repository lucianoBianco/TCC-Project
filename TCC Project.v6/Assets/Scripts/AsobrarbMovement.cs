using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AsobrarbMovement : MonoBehaviour {
	public GameObject path;
	Transform[] waypoints;
	private Transform target;
	private NavMeshAgent agent;
	public GameObject Asobrab;
	 int tracker = 0;
	// Use this for initialization
	void Awake(){
		waypoints = new Transform[path.transform.childCount];
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints [i] = path.transform.GetChild (i);
		}

	}


	void Start () {
		target = waypoints [0];
		agent = GetComponent<NavMeshAgent> ();
		agent.enabled = true;
	}

	// Update is called once per frame
	void Update () {
		if (Asobrab.GetComponent<DialogManager> ().finished) {
			agent.destination = target.position;
			Asobrab.GetComponent<DialogManager> ().finished = false;
			if (tracker < waypoints.Length)
				tracker++;
		}

		float dist = Vector3.Distance (transform.position, target.position);
		if (dist <= 5f) {
			target = waypoints [tracker];
		}
	}


	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
			Asobrab.GetComponent<DialogManager> ().StartDialogue (tracker);
		}
	}

	void OnTriggerExit(Collider other){
		if (other.transform.tag == "Player") {
			Asobrab.GetComponent<DialogManager> ().EndDialogue ();
		}
	}

}
