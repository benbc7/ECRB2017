/*
*	Copyright (C) Ben Cutler
*	Tetricom Studios
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDampToTarget : MonoBehaviour {

	#region Variables

	private Vector3 target;
	public float smoothTime = 0.3f;
	private Vector3 velocity = Vector3.zero;
	private bool lerping;

	#endregion

	void Update () {
		if (transform.position == target) {
			lerping = false;
		}
		if (lerping) {
			transform.position = Vector3.SmoothDamp (transform.position, target, ref velocity, smoothTime);
		}
	}

	#region Functions

	void GetNewTarget (Vector3 position) {
		target = position;
		lerping = true;
	}

	#endregion
}
