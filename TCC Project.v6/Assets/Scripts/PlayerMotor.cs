﻿using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [Header("Init")]
    public GameObject activeModel;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody rb;

    [Header("Inputs")]
    public float horizontal;
    public float vertical;
    public Vector3 movDir = Vector3.zero;
    public float movAmount;

    [Header("Stats")]
    public float speed = 5f;
    public float rotateSpeed = 5f;

    [Header("States")]
    public bool musicMode;


    [HideInInspector]
    public float delta;

    public void Init()
    {
        SetupAnimator();
        rb = GetComponent<Rigidbody>();
        /*
         //Useless with navMesh Agent
        rb.angularDrag = 999;
        rb.drag = 4;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	    */
    }

    void SetupAnimator()
    {
        if (activeModel == null)
        {
            anim = GetComponentInChildren<Animator>();

            if (anim == null)
                Debug.Log("No model found.");
            else
                activeModel = anim.gameObject;
        }

        //if (anim == null)
            //anim = activeModel.GetComponent<Animator>();

        //anim.applyRootMotion = false;
    }

    public void Tick(float d)
    {
		rb.drag = (movAmount > 0) ? 0 : 4;
        /*
        if (movAmount > 0)
            rb.drag = 0;
        else
            rb.drag = 4;
        */
        PerformMovement(d);

        //HandleMovementAnimations(d);
    }

	void PerformMovement(float d){
		if(movDir != Vector3.zero){
			//transform.Translate(-movDir * (speed * movAmount) * d);
			rb.velocity = movDir * (speed*movAmount);

            Vector3 targetDir = movDir;
            targetDir.y = 0;
            //targetDir = transform.forward;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, d * movAmount * rotateSpeed);
            transform.rotation = targetRotation;
		}
	}

    void HandleMovementAnimations(float d)
    {
        anim.SetFloat("vertical", movAmount, 0.4f, d);
    }


    /*Brackeys
    //recebe o vetor de movimento
    public void Move(Vector3 _velocity)
    {

        movDir = _velocity;
    }

    // executar o movimento no update de física
    void FixedUpdate()
    {
        PerformMovement();
    }
    */
}
