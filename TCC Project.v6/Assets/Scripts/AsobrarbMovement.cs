using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class AsobrarbMovement : MonoBehaviour {


    //Classe para tratar a movimentação do NPC Asobrab



	public GameObject path;
	Transform[] waypoints;
	private Transform target;
	private NavMeshAgent agent;
	public GameObject Asobrab;
    private Transform player, oldPlayer;
	private int tracker = 0;
    private bool dialogueOn = false;
    public GameObject balao;
	public bool isMoving = false;
	private DialogManager dMan;
	void Awake(){

        // pegando os pontos do path do npc
		waypoints = new Transform[path.transform.childCount];
		for (int i = 0; i < waypoints.Length; i++) {
			waypoints [i] = path.transform.GetChild (i);
		}

	}


	void Start () {
        //dirigindo o npc para o path inicial definido no load
        if(waypoints.Length > tracker && waypoints[tracker] != null)
		  target = waypoints [tracker];
        else target = null;

		agent = GetComponent<NavMeshAgent> ();
		agent.enabled = true;
		dMan = Asobrab.GetComponent<DialogManager>();
	}

    void Update()
    {
        //caso o diálogo tenha terminado, mover-se para o proximo  ponto
        if (dMan != null && dMan.finished)
        {
			isMoving = true;
            if(target != null)
                agent.destination = target.position;
            dMan.finished = false;
			dMan.deactivate = true;
            if (tracker < waypoints.Length -1)
                tracker++;
        }
        float dist;
        if(target!= null)
            dist = Vector3.Distance(transform.position, target.position);
        else dist = 0;
        if (dist <= 5f)
        {
			isMoving = false;
			dMan.deactivate = false;
            if (waypoints.Length > tracker && waypoints[tracker] != null)
            {
                target = waypoints[tracker];
            }
        }

        //triggers de diálogo
        if (dialogueOn)
        {
            if (Input.GetButtonDown("Ação1"))
            {
                dMan.StartDialogue(tracker, player);
                LockDialog();
            }

            if (dMan.dialogue[tracker].sentences.Length == 0)
            {
                dMan.FinishDialogue();
            }
        }
        transform.LookAt(Camera.main.transform);
    }


	void OnTriggerEnter(Collider other){
        //trigger de diálogo
		if (other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active) {
            player = other.transform;
            balao.SetActive(true);
            if(oldPlayer == null || player.name != oldPlayer.name){
                dMan.CheckFirst(tracker, player);
            }
            oldPlayer = player;
        }

	}

	void OnTriggerExit(Collider other){
		if (other.transform.tag == "Player" && other.gameObject.GetComponent<PlayerController>().myController == PlayerController.Controller.Active) {
            LockDialog();
            balao.SetActive(false);
            player = null;
            oldPlayer = null;
        }

    }

    void LockDialog()
    {
        dialogueOn = false;
    }
    public void UnlockDialog(){
        dialogueOn = true;

    }
	
	public void SetLocation(Vector3 position, int trackerInt){
        // definindo o destino do navmesh
		agent = GetComponent<NavMeshAgent> ();
		agent.enabled = false;
		transform.position = position;
		tracker = trackerInt;
		agent.enabled = true;
	}

}
