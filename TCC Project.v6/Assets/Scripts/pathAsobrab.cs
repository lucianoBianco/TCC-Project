using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathAsobrab : MonoBehaviour {
	float newY;
	float newX;
	bool up = false;
	bool right = false;
	// Use this for initialization
	void Start () {
		newX = transform.localPosition.x;
		newY = transform.localPosition.y;
	}

	// Update is called once per frame
	void Update () {
		if (up)
			newY += 0.3f * Time.deltaTime;
		else
			newY -= 0.3f * Time.deltaTime;
		if (right)
			newX -= 0.15f * Time.deltaTime;
		else
			newX += 0.15f * Time.deltaTime;

		if (newY >= 0.3f)
			up = false;
		else if (newY <= -0.3f)
			up = true;
		if (newX >= 0.3f)
			right = true;
		else if (newX <= -0.3f)
			right = false;

		Vector3 pos = new Vector3 (newX, newY, 0);
		transform.localPosition = pos;

	}
}
