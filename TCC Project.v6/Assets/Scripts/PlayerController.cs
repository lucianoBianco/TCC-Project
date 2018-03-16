using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5f;

	public float turnSmoothTime = .2f;
	float turnSmoothVelocity;
    float xMov;
    float yMov;

    float delta;


    private PlayerMotor motor;

	Camera cam;
	Transform camPosition;
    CameraController camManager;

	void Start () {
        /*//Brackeys
		cam = Camera.main;
		camPosition = Camera.main.transform;
        */

		motor = GetComponent<PlayerMotor>();
        motor.Init();

        camManager = CameraController.singleton;
        camManager.Init(this.transform);

	}


	void Update () {
		//right click
		if(Input.GetMouseButton(1)){
			Ray ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit)){

			}
		}
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
        
    }


    void FixedUpdate()
    {
		delta = Time.fixedDeltaTime;
		camManager.Tick(delta);
		GetInputs();
		UpdateMotor();
		motor.Tick(delta);
    }

    void GetInputs()
    {
        xMov = Input.GetAxisRaw("Horizontal");
        yMov = Input.GetAxisRaw("Vertical");
    }

    void UpdateMotor()
    {
        

        Vector3 v = yMov * camManager.transform.forward;
        Vector3 h = xMov * camManager.transform.right;
        motor.movDir = (v + h).normalized;
        float m = Mathf.Abs(xMov) + Mathf.Abs(yMov);
        motor.movAmount = Mathf.Clamp01(m);

        motor.horizontal = xMov;
        motor.vertical = yMov;

    }
}
