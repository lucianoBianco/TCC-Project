using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsobrabAnim : MonoBehaviour {
	public Transform target;
	public float time = 0.1f;
	private float velocity = 0f;

	void Update(){
		float newX = Mathf.SmoothDamp (transform.localPosition.x, target.localPosition.x, ref velocity, time);
		float newY = Mathf.SmoothDamp (transform.localPosition.y, target.localPosition.y, ref velocity, time);
		transform.localPosition = new Vector3 (newX, newY, transform.localPosition.z);
	}
}
