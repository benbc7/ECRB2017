using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbingCam : MonoBehaviour {

	public float currentSpeedPercentage;
	public float maxSpeed;
	public float maxSpeedScheduledTime;

	private Camera cam;
	private Vector3 targetPosition;
	private Vector3 roundedPosition;
	private bool active;
	private float startTime;

	private void Start () {
		cam = GetComponent<Camera> ();
	}

	// Update is called once per frame
	void Update () {
		if (active) {
			if (currentSpeedPercentage < 1)
				currentSpeedPercentage = ((Time.time - startTime) / maxSpeedScheduledTime);
			else
				currentSpeedPercentage = 1;

			//targetPosition += (Vector3.up * (Time.deltaTime * maxSpeed * currentSpeedPercentage));
			//roundedPosition = new Vector3 (RoundToNearestPixel (targetPosition.x), RoundToNearestPixel (targetPosition.y), -10);
			//transform.position = roundedPosition;

			transform.Translate (Vector3.up * (Time.deltaTime * maxSpeed * currentSpeedPercentage), Space.World);
		}
	}

	public float RoundToNearestPixel (float unityUnits) {
		float valueInPixels = (Screen.height / (cam.orthographicSize * 2)) * unityUnits;
		valueInPixels = Mathf.Round (valueInPixels);
		float adjustedUnityUnits = valueInPixels / (Screen.height / (cam.orthographicSize * 2));
		return adjustedUnityUnits;
	}

	void Activate () {
		active = true;
		startTime = Time.time;
	}
}
