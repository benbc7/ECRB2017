/*
*	Copyright (C) Ben Cutler
*	Tetricom Studios
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampToTarget : MonoBehaviour {

	#region Variables

	public Transform target;
	public float zPosition;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;

	#endregion

	void Update () {
		if (transform.position.x != target.position.x && transform.position.y != target.position.y) {
			Vector3 position = new Vector3 (target.position.x, target.position.y, zPosition);
			transform.position = Vector3.SmoothDamp (transform.position, position, ref velocity, smoothTime);
		}
	}

	#region Functions

	void GetNewTarget (Transform newTarget) {
		target = newTarget;
	}

	#endregion
}
