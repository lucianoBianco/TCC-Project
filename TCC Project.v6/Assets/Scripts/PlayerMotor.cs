using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour {

// gerenciador de movimentação do personagem
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
    public float speed = 8f;
    public float rotateSpeed = 7f;
	public Vector3 movFINAL = Vector3.zero;
	public float toGround = 0.5f;


    [Header("States")]
    public bool musicMode;
	public bool lockOn;
    public bool movBlocked = false;
	public bool puzzleBlocked = false;
	public bool onGround;

    [Header("Other")]
	public GameObject lockonTarget;

	//float dist;
    [HideInInspector]
    public float delta;
	[HideInInspector]
	public GameObject playerAtual;
	[HideInInspector]
	public LayerMask ignoreLayers;

    Vector3 movRB;
	float currentSpeed;
	Vector3 lastPosition;
	private float positionY;

    public void Init()
    {
        SetupAnimator();
		agent = GetComponent<NavMeshAgent>();
		rb = GetComponent<Rigidbody> ();
		//Useless with navMesh Agent

		rb.angularDrag = 999;
		//rb.drag = 4;
		rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		gameObject.layer = 8;
		ignoreLayers = ~(1 << 9);
        agent.enabled = false;
	    
    }
	void Start(){
		
		if (playerAtual == null){
			playerAtual = GameObject.Find ("Johanna");
		}
		if (playerAtual.GetComponent<PlayerController> ().myController == PlayerController.Controller.Inactive) {
			foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
                if (player.GetComponent<PlayerController>() != null && player.GetComponent<PlayerController>().myController == PlayerController.Controller.Active)
                    playerAtual = player;
			}
		}
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
		onGround = OnGround ();
    	PerformMovement(d);
    }

    void FixedUpdate(){
		positionY = transform.position.y;
    }
	

	void PerformMovement(float d){
		if (transform.GetComponent<PlayerController> ().myController == PlayerController.Controller.Active) {

			// moviemntação ativa do jogador
            agent.enabled = false;
			
			
			delta = d;

        	movAmount = (!movBlocked) ? movAmount : 0;
			movFINAL = movDir * (speed * movAmount);// * delta;
			movFINAL.y = rb.velocity.y;
			if (onGround){
				rb.velocity = movFINAL;
			}
			else {
				movFINAL.y = 0;
				float velocidadeY = rb.velocity.y;
				rb.AddForce (movFINAL * 3);
				rb.velocity = Vector3.ClampMagnitude(rb.velocity, 5);
				Vector3 velTemp = rb.velocity;
				velTemp.y = velocidadeY;
				rb.velocity = velTemp;
			}

			
			Vector3 targetDir = /*(!lockOn)? movDir : lockonTarget.transform.position - transform.position;*/movDir;
			targetDir.y = 0;
			//targetDir = transform.forward;
			if(targetDir.magnitude > 0){
				Quaternion tr = Quaternion.LookRotation (targetDir);
				Quaternion targetRotation = Quaternion.Slerp (transform.rotation, tr, delta * movAmount * rotateSpeed);
				transform.rotation = targetRotation;
			}
		}else if(transform.GetComponent<PlayerController> ().myController == PlayerController.Controller.Inactive){

			//movimentaç~~ao do personagem pela IA
			if(!puzzleBlocked){
				agent.enabled = true;
				agent.destination = playerAtual.transform.position;
				agent.stoppingDistance = 3f;
				currentSpeed = Mathf.Lerp (currentSpeed, (transform.position - lastPosition).magnitude / Time.deltaTime, 0.25f);
				lastPosition = transform.position;
				movAmount = currentSpeed;
			}
		}
    }

	public void Jump(bool isJump){
        
        // animação de pulo e falha nos puzzles de luz em caso de pulo
		if (isJump && onGround && !movBlocked) {
            rb.AddForce ((Vector3.up) * 6, ForceMode.Impulse);
            anim.SetTrigger("Jump");
			if(gameObject.GetComponent<JohannaSkills> () != null){
				PuzzleLuz[] puzzles = gameObject.GetComponent<JohannaSkills> ().puzzles;
				for (int i = 0; i < puzzles.Length; i++) {
					puzzles [i].ResetPuzzle ();
				}
			}
		}


	
	}
	public bool OnGround(){

		// raycast para definir se o personagem está no chão
		bool r = false;

		Vector3 origin = transform.position + (Vector3.up * toGround);
		Vector3 dir = -Vector3.up;
		float dis = toGround + 0.1f;
		Debug.DrawRay (origin, dir * dis);
		RaycastHit hit;
		if (Physics.Raycast (origin, dir, out hit, dis, ignoreLayers)) {
			r = true;
			Vector3 targetPosition = hit.point;
		}
		if(transform.position.y == positionY){
			r = true;
		}
		if(!agent.enabled)
        	anim.SetBool("OnGround", r);
		return r;
	}
}
