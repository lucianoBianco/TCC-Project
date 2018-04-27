using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static CameraController singleton;
    public Transform target;
	public Transform lockonTarget;
    private Transform pivot;
    private Transform camTrans;


    public float mouseSensitivty = 2f;
    public float controllerSensitivity = 5f;

    public float followSpeed = 3f;

	public Vector3 dstOffset = new Vector3(0,-2.53f,5.07f);
	public Vector2 pitchMinMax = new Vector2 (-35, 35);

	public float zoomSpeed = 4f;
	//private float currentZoom = 10f;
	public Vector2 zoomMinMax = new Vector2 (5f,15f);

	public float rotationSmoothTime = .12f;

    private Vector3 rotationSmoothVelocity;
	private Vector3 currentRotation;

    //Brackeys
    private float yaw;
    private float pitch;
    //Sharp Accent
    private float lookAngle;
    private float tiltAngle;
	public bool lockOn;

    private float smoothX;
    private float smoothXvelocity;
    private float smoothY;
    private float smoothYvelocity;


    void Awake()
    {
        singleton = this;
    }

    public void Init(Transform t)
    {
        target = t;
        camTrans = Camera.main.transform;
        pivot = camTrans.parent;
    }

    public void Tick(float d)
    {
        float h = Input.GetAxis("Mouse X");
        float v = Input.GetAxis("Mouse Y");

        float c_h = Input.GetAxis("RightAxis X");
        float c_v = Input.GetAxis("RightAxis Y");

        float targetSpeed = mouseSensitivty;

        if(c_h != 0 || c_v != 0)
        {
            h = c_h;
            v = c_v;
            targetSpeed = controllerSensitivity;
        }

        FollowTarget(d);
        HandleRotation(h, v, d, targetSpeed);
    }

    void FollowTarget(float d)
    {
        float speed = followSpeed * d * 2;
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, speed);
        transform.position = targetPosition;

        //transform.position = target.position - currentZoom;

	 	//currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
	 	//currentZoom = Mathf.Clamp(currentZoom, zoomMinMax.x, zoomMinMax.y);
    }

    void HandleRotation(float h, float v, float d, float targetSpeed)
    {
        if(rotationSmoothTime > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, h, ref smoothXvelocity, rotationSmoothTime);
            smoothY = Mathf.SmoothDamp(smoothY, v, ref smoothYvelocity, rotationSmoothTime);
        }
        else
        {
            smoothX = h;
            smoothY = v;
		}

		tiltAngle -= smoothY * targetSpeed;
		tiltAngle = Mathf.Clamp(tiltAngle, pitchMinMax.x, pitchMinMax.y);
		pivot.localRotation = Quaternion.Euler (tiltAngle, 0, 0);

		//if the camera is locked on a specific target
		if (lockOn && lockonTarget != null)
        {
			Vector3 targetDir = lockonTarget.position - transform.position;
			targetDir.Normalize ();
			//targetDir.y = 0;
			if (targetDir == Vector3.zero) {
				targetDir = transform.forward;
			}
			Quaternion targetRot = Quaternion.LookRotation (targetDir);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRot, d * 9);


			return;
        }

        lookAngle += smoothX * targetSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);

    }



    /*void LateUpdate(){
	 	yaw += Input.GetAxis("Mouse X") * mouseSensitivty;
	 	pitch -= Input.GetAxis("Mouse Y") * mouseSensitivty;
	 	pitch = Mathf.Clamp (pitch , pitchMinMax.x, pitchMinMax.y);

	 	currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3 (pitch, yaw), ref rotationSmoothVelocity, rotationSmoothTime);

	 	transform.eulerAngles = currentRotation;

	 	transform.position = target.position - transform.forward /*- dstOffset*/ /** currentZoom;

	 	currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
	 	currentZoom = Mathf.Clamp(currentZoom, zoomMinMax.x, zoomMinMax.y);
    	
    }*/

}
