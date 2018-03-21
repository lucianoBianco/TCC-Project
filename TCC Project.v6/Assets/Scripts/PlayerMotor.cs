using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

    [Header("Init")]
    public GameObject activeModel;
    [HideInInspector]
    public Animator anim;
	[HideInInspector]
	public NavMeshAgent agent;
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
	public Vector3 movFINAL = Vector3.zero;


    [Header("States")]
    public bool musicMode;

	
    [HideInInspector]
    public float delta;


    Vector3 movRB;
    bool jump = false;

    public void Init()
    {
        SetupAnimator();
		agent = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody> ();
		//Useless with navMesh Agent
	    
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
        PerformMovement(d);
    }

	void PerformMovement(float d){
		if(movDir != Vector3.zero){
            //transform.Translate(-movDir * (speed * movAmount) * d);
            //rb.velocity = movDir * (speed*movAmount);
            delta = d;
            
            if (!jump)
            {
                movFINAL = movDir * (speed * movAmount) * delta;
                
                agent.Move(movFINAL);
            }
            else
            { }
			Vector3 targetDir = movDir;
            targetDir.y = 0;
            //targetDir = transform.forward;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(activeModel.transform.rotation, tr, delta * movAmount * rotateSpeed);
			activeModel.transform.rotation = targetRotation;
		}
        else
        movFINAL = Vector3.zero;


        movRB = movFINAL;
    }

	public void Jump(bool isJump){
        
		if (isJump && !jump) {
			jump = true;
			print ("pula, viado");
			agent.enabled = false;
            rb.velocity = Vector3.zero;
            float rbY = rb.velocity.y;
            float rbX = movRB.x / delta;
            float rbZ = movRB.z / delta;
            Vector3 newVel = new Vector3(rbX, rbY, rbZ);
            rb.velocity = newVel;
            rb.AddForce ((Vector3.up) * 400);
			isJump = false;
		}


	}

	void OnCollisionEnter(Collision collision){

		if (collision.gameObject.tag == "Ground") {
			agent.enabled = true;
            rb.velocity = Vector3.zero;
			jump = false;
		}

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
