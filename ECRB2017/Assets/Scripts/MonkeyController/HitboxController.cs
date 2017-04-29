using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour {

	private void OnTriggerEnter2D (Collider2D other) {
		if (other.transform != transform.parent && other.tag == "Player") {
			other.SendMessage ("StunPlayer", 0.2f);

			int hitDirection = (transform.localPosition.x > 0) ? 1 : -1;

			other.SendMessage ("KnockBackPlayer", hitDirection);
		}
	}
}
