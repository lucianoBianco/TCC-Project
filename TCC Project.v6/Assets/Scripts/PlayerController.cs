using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	// controla os inputs de movimentação dos personagens
    public enum Controller { Active, Inactive }
    [SerializeField]
    //private float speed = 5f;
    public Controller myController;
	public float turnSmoothTime = .2f;
	float turnSmoothVelocity;
    float xMov;
    float yMov;
	TommySkills skillScript;

    float delta;

    public bool isDefault = false;

	bool rightAxis_down;

    private PlayerMotor motor;
    private GameObject interactibleFocus;

	Camera cam;
	Transform camPosition;
    CameraController camManager;
    public Properties props;
    

    void Start () {
        /*//Brackeys
		cam = Camera.main;
		camPosition = Camera.main.transform;
        */

		motor = GetComponent<PlayerMotor>();
		motor.Init();
        camManager = CameraController.singleton;
        if (isDefault)
        {
            myController = Controller.Active;
            camManager.Init(this.transform);
			motor.playerAtual = transform.gameObject;
        }
        else
            myController = Controller.Inactive;
	}


	void Update () {
		//right click
		/*
		if(Input.GetMouseButton(1)){
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){

			}
		}*/
        /*
		//recebendo os inputs de movimentação
		xMov = Input.GetAxisRaw("Horizontal");
		zMov = Input.GetAxisRaw("Vertical");
		Vector2 mov = new Vector2 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
		Vector2 movDir = mov.normalized;

		Vector3 movHorizontal = transform.right * xMov;
		Vector3 movVertical = transform.forward * zMov;

		//Vetor final de movimentação
		Vector3 velocity = (movHorizontal+movVertical).normalized ;//* speed;
		if(velocity != Vector3.zero){
			float targetRotation = Mathf.Atan2 (movDir.x, movDir.y) * Mathf.Rad2Deg + camPosition.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		//aplicando a movimentação

		motor.Move(velocity);
        */


        // versão SOULS-LIKE
        //GetInputs();
        delta = Time.deltaTime;

		//camManager.Tick(delta);
		if (!GlobalVariablesCaverna.isPaused) {
			switch (myController) {
			case Controller.Active:
				camManager.Tick (delta);
				GetInputs ();
				UpdateMotor ();
				break;
			case Controller.Inactive:

				break;
			}
			motor.Tick (delta);
		}
    }


    void FixedUpdate()
    {
        
    }
// registra os inputs de moviemntação
    void GetInputs()
    {
		xMov = Input.GetAxisRaw ("Horizontal");
		yMov = Input.GetAxisRaw ("Vertical");
		motor.Jump (Input.GetButtonDown ("Jump"));
		if (myController == Controller.Active) {
			if (props != null) {
				props.Action (Input.GetButtonDown ("Ação1"));
			}
        }

		rightAxis_down = Input.GetButtonUp ("Fire2");
    }
// atualiza os valores de movimento referente aos inputs
    void UpdateMotor()
    {
        

        Vector3 v = yMov * camManager.transform.forward;
        Vector3 h = xMov * camManager.transform.right;
        motor.movDir = (v + h).normalized;
        float m = Mathf.Abs(xMov) + Mathf.Abs(yMov);
        motor.movAmount = Mathf.Clamp01(m);

        motor.horizontal = xMov;
        motor.vertical = yMov;


		if (rightAxis_down) {
			motor.lockonTarget = SwitchFocus.TrocaAlvo();
			if (motor.lockonTarget != null){
				motor.lockOn = true;
			}
			else{
				motor.lockOn = false;
			}
			camManager.lockonTarget = motor.lockonTarget;
			camManager.lockOn = motor.lockOn;
		}
		

        

    }

// libera a interação em caso de proximidade
    public void OnTriggerEnter(Collider other)
    {
        if (myController == Controller.Active)
        {
            if (other.gameObject.tag == ("Interactible"))
            {
                if (interactibleFocus == null)
                    interactibleFocus = other.gameObject;
            }
        }
    }
 // desativa interação com objetos
    private void OnTriggerExit(Collider other)
    {
        if (myController == Controller.Active)
        {
            if (other.gameObject == interactibleFocus)
            {
                interactibleFocus = null;
            }
        }
    }
// zera os valores de lock on de camera
	public void ResetLockon(){
		if(motor != null){
			motor.lockonTarget = null;
			motor.lockOn = false;
		}
		if(camManager != null){
			camManager.lockonTarget = null;
			camManager.lockOn = false;
		}
	}
}
