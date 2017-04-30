using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class climbingCam : MonoBehaviour {

	public float currentSpeedPercentage;
	public float maxSpeed;
	public float maxSpeedScheduledTime;

	private bool active;
	private float startTime;

	// Update is called once per frame
	void Update () {
		if (active) {
			if (currentSpeedPercentage < 1)
				currentSpeedPercentage = ((Time.time - startTime) / maxSpeedScheduledTime);
			else
				currentSpeedPercentage = 1;

			transform.Translate (Vector3.up * (Time.deltaTime * maxSpeed * currentSpeedPercentage), Space.World);
		}
	}

	void Activate () {
		active = true;
		startTime = Time.time;
	}
}
