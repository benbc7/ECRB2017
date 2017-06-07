using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class MonkeyPlayer : MonoBehaviour {

    public float moveSpeed = 6;
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = 0.4f;
    float accelerationTimeAirborne = 0.2f;
    float accelerationTimeGrounded = 0.1f;

    public Vector2 wallJumpClimb = new Vector2 (7.5f, 16f);
    public Vector2 wallJumpOff = new Vector2 (8.5f, 7f);
    public Vector2 wallLeap = new Vector2 (18, 17);
    public float wallSlideSpeedMax = 3;
    public float wallStickTime = 0.25f;
    float timeToWallUnstick;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    float velocityXSmoothing;

    public Vector3 velocity;

    Controller2D controller;

	MonkeyAudioManager audioManager;

    Vector2 directionalInput;

	private Transform hitbox;
	private Vector3 hitboxPosition = new Vector3 (0.3f, -0.1f, 0);

	[System.NonSerialized]
    public bool wallSliding;
	[System.NonSerialized]
	public bool grabbingLedge;
    int wallDirectionX;

	void Start () {
        controller = GetComponent<Controller2D> ();
		audioManager = GetComponent<MonkeyAudioManager> ();

		hitbox = transform.Find ("AttackHitbox");

        gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs (gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
    }

    void Update () {
        CalculateVelocity ();
        //HandleWallSliding ();

        controller.Move (velocity * Time.deltaTime, directionalInput);
        controller.animator.SetFloat ("velocityX", Mathf.Abs (velocity.x));
        controller.animator.SetFloat ("velocityY", velocity.y);
		float walkSpeed = Mathf.Abs (velocity.x) / 5f;
		controller.animator.SetFloat ("walkSpeed", (walkSpeed < 0.01f) ? 1 : walkSpeed);
		//controller.animator.SetBool ("facingRight", Mathf.Sign (velocity.x) == 1);

        if (controller.collisions.above || controller.collisions.below) {
            if (controller.collisions.slidingDownMaxSlope) {
                velocity.y += controller.collisions.slopeNormal.y * -gravity * Time.deltaTime;
            } else {
                velocity.y = 0;
            }
        }
    }

    public void SetDirectionalInput (Vector2 input) {
        directionalInput = input;
		if (input.x != 0) {
			controller.animator.SetBool ("facingRight", Mathf.Sign (input.x) == 1);
			hitbox.localPosition = new Vector3 (hitboxPosition.x * Mathf.Sign (input.x), hitboxPosition.y, hitboxPosition.z);
		}
    }

    public void OnJumpInputDown () {
        if (wallSliding) {
            if (wallDirectionX == directionalInput.x) {
                velocity.x = -wallDirectionX * wallJumpClimb.x;
                velocity.y = wallJumpClimb.y;
                controller.animator.Play ("Jump");
				if (Random.Range (0,10) > 7) {
					audioManager.PlayMonkeyChatter ();
				}
				controller.UpdateSpriteFaceDirection (-Mathf.Sign (velocity.x));
			} else if (directionalInput.x == 0) {
                velocity.x = -wallDirectionX * wallJumpOff.x;
                velocity.y = wallJumpOff.y;
                controller.animator.Play ("Jump");
				if (Random.Range (0, 10) > 7) {
					audioManager.PlayMonkeyChatter ();
				}
				controller.UpdateSpriteFaceDirection (Mathf.Sign (velocity.x));
			} else {
                velocity.x = -wallDirectionX * wallLeap.x;
                velocity.y = wallLeap.y;
                controller.animator.Play ("Jump");
				if (Random.Range (0, 10) > 7) {
					audioManager.PlayMonkeyChatter ();
				}
				controller.UpdateSpriteFaceDirection (Mathf.Sign (velocity.x));
			}
        }
        if (controller.collisions.below) {
            if (controller.collisions.slidingDownMaxSlope) {
                if (directionalInput.x != -Mathf.Sign (controller.collisions.slopeNormal.x)) { // not jumping against max slope
                    velocity.y = maxJumpVelocity * controller.collisions.slopeNormal.y;
                    velocity.x = maxJumpVelocity * controller.collisions.slopeNormal.x;
                    controller.animator.Play ("Jump");
					if (Random.Range (0, 10) > 7) {
						audioManager.PlayMonkeyChatter ();
					}
				}
            } else  {
                velocity.y = maxJumpVelocity;
                controller.animator.Play ("Jump");
				if (Random.Range (0, 10) > 7) {
					audioManager.PlayMonkeyChatter ();
				}
			}
        }
    }

    public void OnJumpInputUp () {
        if (velocity.y > minJumpVelocity) {
            velocity.y = minJumpVelocity;
        }
    }

	public void KnockBackPlayer (int hitDirection) {
		if (Random.Range (0, 10) > 7) {
			audioManager.PlayMonkeyChatter ();
		}
		if (Random.Range (0, 10) > 5) {
			audioManager.PlayHits ();
		}
		float xMultiplier = Mathf.Abs (velocity.x / 4) + 1;
		velocity.x = minJumpVelocity * hitDirection * xMultiplier / 2;
		velocity.y = minJumpVelocity / 3f;
	}

	public void OnNormalAttackInput (int comboNumber) {
        float xMultiplier = Mathf.Abs (velocity.x / 8) + 1;
        velocity.x = minJumpVelocity * controller.collisions.faceDirection * xMultiplier / 3;
		controller.animator.SetTrigger ("Punch");
		audioManager.PlayWhoosh ();
	}

    public void OnForwardAttackInput (int faceDirection) {
        if (!controller.collisions.slidingDownMaxSlope) {
            float xMultiplier = Mathf.Abs (velocity.x / 16) + 1;
            velocity.x = minJumpVelocity * faceDirection * xMultiplier;
            controller.animator.SetTrigger ("ForwardAttack");
			audioManager.PlayWhoosh ();
		}
    }

    public void OnUpAttackInput (int faceDirection) {
        velocity.x = minJumpVelocity / 2 * faceDirection;
        velocity.y = maxJumpVelocity;
        controller.animator.SetTrigger ("UpAttack");
		audioManager.PlayWhoosh ();
	}

    public void OnDownAttackInput () {
        if (!controller.collisions.below) {
            velocity.y = minJumpVelocity / -1f;
			audioManager.PlayWhoosh ();
        }
        controller.animator.SetTrigger ("DownAttack");
    }

	public void OnRollInput (int faceDirection) {
		faceDirection = (faceDirection != 0) ? faceDirection : controller.collisions.faceDirection;
		velocity.x = maxJumpVelocity * 0.9f * faceDirection;
		velocity.y = minJumpVelocity / 3f;
		controller.animator.Play ("Roll");
		audioManager.PlayGroundPound ();
	}

    void HandleWallSliding () {
        wallDirectionX = (controller.collisions.left) ? -1 : 1;
        wallSliding = false;
        controller.animator.SetBool ("wallSliding", false);
        if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
            wallSliding = true;
            controller.animator.SetBool ("wallSliding", true);

            if (velocity.y < -wallSlideSpeedMax) {
                velocity.y = -wallSlideSpeedMax;
            }

            if (timeToWallUnstick > 0) {
                velocityXSmoothing = 0;
                velocity.x = 0;

                if (directionalInput.x != wallDirectionX && directionalInput.x != 0) {
                    timeToWallUnstick -= Time.deltaTime;
                } else {
                    timeToWallUnstick = wallStickTime;
                }
            } else {
                timeToWallUnstick = wallStickTime;
            }
        }
    }

	void HandleEdgeGrabbing () {
		wallDirectionX = (controller.collisions.left) ? -1 : 1;
		grabbingLedge = false;
		controller.animator.SetBool ("grabbingLedge", true);


	}

    void CalculateVelocity () {
        float targetVelocityX = directionalInput.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
    }
}
