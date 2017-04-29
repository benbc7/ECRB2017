using UnityEngine;
using System.Collections;
using Rewired;

[RequireComponent (typeof (MonkeyPlayer))]
public class PlayerInput : MonoBehaviour {

	public int playerNumber;

	Player joystick;

	Vector2 directionalInput;

	bool charging;
	bool attacking;
	bool readyToRoll = true;
	bool stunned;

	float attackTimer;
	float rollTimer;
	float stunTimer;

	float joystickAttackDeadZone = 0.3f;

	MonkeyPlayer player;
	Controller2D controller2D;

	void Start () {
		joystick = ReInput.players.GetPlayer (playerNumber);
		player = GetComponent<MonkeyPlayer> ();
		controller2D = GetComponent<Controller2D> ();
	}

	void Update () {
		UpdateTimers ();

		MovementInput ();

		if (!stunned) {
			AttackInput ();
		}
	}

	void MovementInput () {
		if (!charging && !attacking && !stunned) {
			directionalInput = new Vector2 (joystick.GetAxisRaw ("Horizontal"), joystick.GetAxisRaw ("Vertical"));
			player.SetDirectionalInput (directionalInput);
			if (directionalInput.x != 0 && controller2D.collisions.below) {
				if (!player.wallSliding) {
					controller2D.UpdateSpriteFaceDirection (directionalInput.x);
				}
			}

			if (readyToRoll) {
				RollInput ();
			}

			if (joystick.GetButtonDown ("Jump")) {
				player.OnJumpInputDown ();
			}
			if (joystick.GetButtonUp ("Jump")) {
				player.OnJumpInputUp ();
			}
		} else {
			directionalInput = Vector2.zero;
			player.SetDirectionalInput (directionalInput);
		}
	}

	void AttackInput () {
		if (joystick.GetButtonDown ("Attack") && !attacking) {
			if (directionalInput.x <= joystickAttackDeadZone && directionalInput.x >= -joystickAttackDeadZone && directionalInput.y <= joystickAttackDeadZone && directionalInput.y >= -joystickAttackDeadZone) { // normal attack
				attacking = true;
				attackTimer = Time.time + 0.1f;
				player.OnNormalAttackInput (1);
			}
			if ((directionalInput.x > joystickAttackDeadZone || directionalInput.x < -joystickAttackDeadZone) && directionalInput.y < joystickAttackDeadZone && directionalInput.y > -joystickAttackDeadZone) { // attack forward
				attacking = true;
				attackTimer = Time.time + 0.25f;
				player.OnForwardAttackInput ((int) Mathf.Sign (directionalInput.x));
			}
			if (directionalInput.y > joystickAttackDeadZone && directionalInput.x < joystickAttackDeadZone && directionalInput.x > -joystickAttackDeadZone) { // attack up
				attacking = true;
				attackTimer = Time.time + 0.5f;
				player.OnUpAttackInput ((int) Mathf.Sign (directionalInput.x));
			}
			if (directionalInput.y < -joystickAttackDeadZone && directionalInput.x < joystickAttackDeadZone && directionalInput.x > -joystickAttackDeadZone) { // attack down
				attacking = true;
				attackTimer = Time.time + 0.25f;
				player.OnDownAttackInput ();
				if (controller2D.collisions.below) {
				}
			}
		}
	}

	void RollInput () {
		if (joystick.GetButtonDown ("Roll")) {
			if ((directionalInput.x > joystickAttackDeadZone || directionalInput.x < -joystickAttackDeadZone) && directionalInput.y < joystickAttackDeadZone && directionalInput.y > -joystickAttackDeadZone) {
				readyToRoll = false;
				rollTimer = Time.time + 0.35f;
				player.OnRollInput ((int) Mathf.Sign (directionalInput.x));
			}
		}
	}

	public void StunPlayer (float stunDuration) {
		stunned = true;
		stunTimer = Time.time + stunDuration;
		controller2D.animator.SetTrigger ("stun");
	}

	void UpdateTimers () {
		if (attackTimer != -1) {
			if (attackTimer <= Time.time) {
				attacking = false;
				attackTimer = -1;
			}
		}
		if (rollTimer != -1) {
			if (rollTimer <= Time.time) {
				readyToRoll = true;
				rollTimer = -1;
			}
		}
		if (stunTimer != -1) {
			if (stunTimer <= Time.time && stunned == true) {
				stunned = false;
				controller2D.animator.SetTrigger ("unstun");
				stunTimer = -1;
			}
		}
	}
}
