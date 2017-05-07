using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationaryPlatformController : RaycastController {

	public LayerMask passengerMask;

	private List<Transform> passengers;
	private List<Transform> passengersOld = new List<Transform> ();

	private void Update () {

		UpdateRaycastOrigins ();

		GetPassengers ();

	}

	public bool StandingOnPlatform (Transform player) {
		if (passengers.Contains (player)) {
			return true;
		} else {
			return false;
		}
	}

	private void GetPassengers () {
		passengersOld = passengers;
		passengers = new List<Transform> ();

		float rayLength = skinWidth;

		for (int i = 0; i < verticalRayCount; i++) {
			Vector2 rayOrigin = raycastOrigins.topLeft;
			rayOrigin += Vector2.right * (verticalRaySpacing * 1);
			RaycastHit2D hit = Physics2D.Raycast (rayOrigin, Vector2.up, rayLength, passengerMask);

			if (hit) {
				passengers.Add (hit.transform);
				if (passengersOld.Contains (hit.transform)) {
					NewPassengerLanded ();
				}
			}
		}
	}

	private void NewPassengerLanded () {
		print ("New Passenger");
	}
}
