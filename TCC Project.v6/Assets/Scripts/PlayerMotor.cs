using UnityEngine;
using UnityEngine.AI;

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

	bool jump = false;
    [HideInInspector]
    public float delta;

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
			movFINAL = movDir * (speed * movAmount) * d;
			if(!jump)
			agent.Move(movFINAL);
			Vector3 movRB = movFINAL;
			movRB.y = 0;
			rb.velocity = movRB;
			Vector3 targetDir = movDir;
            targetDir.y = 0;
            //targetDir = transform.forward;
            Quaternion tr = Quaternion.LookRotation(targetDir);
            Quaternion targetRotation = Quaternion.Slerp(activeModel.transform.rotation, tr, d * movAmount * rotateSpeed);
			activeModel.transform.rotation = targetRotation;
		}
	}

	public void Jump(bool isJump){
		if (isJump) {
			jump = true;
			print ("pula, viado");
			agent.enabled = false;
			rb.AddForce ((Vector3.up*400)+movFINAL*50);

			print (rb.velocity);
			isJump = false;
			//yield return new WaitForSeconds (0.5f);
		}


	}

	void OnCollisionEnter(Collision collision){

		if (collision.gameObject.tag == "Ground") {
			agent.enabled = true;
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
