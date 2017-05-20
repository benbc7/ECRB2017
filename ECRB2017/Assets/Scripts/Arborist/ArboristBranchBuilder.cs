using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;
using UnityEngine.UI;
public class ArboristBranchBuilder : MonoBehaviour {

	//branch vars

	[System.NonSerialized]
	public bool menuMode = true;
	private Transform selectedButton;
	public LayerMask menuButtonMask;

	public bool canSpawn = false;
	public int branchIndex = 1;
	public int maxBranchIndex;
	public List<GameObject> branches;
	public Image[] branchButtons;
	public SpriteRenderer branchSR;
	public GameObject masterBranch;
	public GameObject currentBranch;
	public Transform branchSpawnLoc;
	public NodePrefab NP;


	//cursor movement vars

	public bool lockedOnNode;
	public float moveSpeed;
	public float joystickDeadZone;
	Vector2 directionalInput;
	Rigidbody2D rb;

	Player joystick;

	//Arborist Animation.

	public bool fistDrop;
	public float fistTimer;
	public Sprite leftFist;
	public Sprite leftHand;
	public Sprite rightFist;
	public Sprite rightHand;
	public SpriteRenderer LHSR;
	public SpriteRenderer RHSR;

	//UpdateSpriteAndButtons(int index) is how you change branches.

	// Use this for initialization
	void Start () {

		maxBranchIndex = branchButtons.Length - 1;
		rb = gameObject.GetComponent<Rigidbody2D> ();
		joystick = ReInput.players.GetPlayer (0);
	}

	//MOVEMENT
	void FixedUpdate () {
		directionalInput = new Vector2 (joystick.GetAxisRaw ("CursorHorizontal"), joystick.GetAxisRaw ("CursorVertical"));
		Vector3 targetPos = new Vector3 ();
		if (directionalInput.x <= joystickDeadZone && directionalInput.x >= -joystickDeadZone)
			rb.AddForce (Vector2.right * directionalInput.x * moveSpeed * Time.deltaTime);
		if (directionalInput.y <= joystickDeadZone && directionalInput.y >= -joystickDeadZone)
			rb.AddForce (Vector2.up * directionalInput.y * moveSpeed * Time.deltaTime);



		targetPos.x += directionalInput.x * moveSpeed * Time.deltaTime;
		targetPos.y += directionalInput.y * moveSpeed * Time.deltaTime;
		targetPos.z = 0f;


		transform.position += targetPos;

	}
	// Update is called once per frame
	void Update () {
		if (fistDrop == true) {
			fistTimer -= Time.deltaTime;
			if (fistTimer <= 0) {
				fistTimer = 0;
				fistDrop = false;
				UpdateHands (leftHand, rightHand);

			}
		}

		if (menuMode) {
			MenuRaycaster ();
		}

		//INPUTS
		if (joystick.GetButtonDown ("PlaceBranch") && lockedOnNode == true) {
			if (currentBranch == null) {
				GameObject B = Instantiate (masterBranch, branchSpawnLoc.transform.position, branchSpawnLoc.transform.rotation);
				currentBranch = B;
				B.GetComponent<Rigidbody2D> ().isKinematic = true;



				maxBranchIndex = branchButtons.Length - 1;
				//maxBranchIndex = currentBranch.transform.GetChild(0).childCount;
				Transform t = currentBranch.transform;
				foreach (Transform child in t.GetChild (0)) {
					branches.Add (child.gameObject);
				}

				if (currentBranch.transform.position.x < 0) {
					FlipBranch ();
				}
				UpdateBranchAndButtons (branchIndex);
			} else {
				NP.cirCol.enabled = false;
				NP = null;
				lockedOnNode = false;
				moveSpeed = 3;
				AlphaTransition (1f);
				branches [branchIndex].transform.GetChild (0).gameObject.SetActive (true);
				branches.Clear ();
				currentBranch = null;
				fistDrop = true;
				fistTimer = 0.5f;
				if (gameObject.transform.position.x >= 0)
					UpdateHands (leftHand, rightFist);
				else
					UpdateHands (leftFist, rightHand);
			}
		}
		if (joystick.GetButtonDown ("PreviousBranch") && currentBranch != null) {
			if (branchIndex == 0) {
				branchIndex = maxBranchIndex;
			} else {
				branchIndex--;
			}
			UpdateBranchAndButtons (branchIndex);
		}
		if (joystick.GetButtonDown ("NextBranch") && currentBranch != null) {
			if (branchIndex == maxBranchIndex) {
				branchIndex = 0;

			} else {
				branchIndex++;
			}

			UpdateBranchAndButtons (branchIndex);
		} else if (Input.GetKeyDown (KeyCode.LeftShift) || (joystick.GetButtonDown ("Cancel")) && lockedOnNode == true) {
			branches.Clear ();
			NP = null;
			lockedOnNode = false;
			Destroy (currentBranch);
			moveSpeed = 3;
			currentBranch = null;
			//AlphaTransition(0.5f);
		}
	}

	void MenuRaycaster () {
		RaycastHit hit;
		if (Physics.Raycast (transform.position, Vector3.forward, out hit, 2f, menuButtonMask)) {
			if (selectedButton == null) {
				selectedButton = hit.transform;
				selectedButton.SendMessage ("SelectButton");
			}

			if (joystick.GetButtonDown ("PlaceBranch")) {
				selectedButton.SendMessage ("Confirm");
			}
		} else {
			if (selectedButton != null) {
				selectedButton.SendMessage ("DeselectButton");
				selectedButton = null;
			}
		}
	}

	//
	//sprite and positioning functions
	//
	public void FlipBranch () {
		currentBranch.transform.localScale = new Vector3 (-1, 1, 1);
	}
	public void UpdateHands (Sprite _leftHand, Sprite _rightHand) {
		LHSR.sprite = _leftHand;
		RHSR.sprite = _rightHand;
	}

	void UpdateBranchAndButtons (int index) {
		Color _color;
		float r, g, b, a;

		for (int i = 0; i <= maxBranchIndex; i++) {
			//physical branches
			branches [i].SetActive (false);
			branchSR = branches [i].GetComponent<SpriteRenderer> ();
			AlphaTransition (1);
			//branch UI
			r = g = b = 255f / 255f;
			a = 50f / 255f;
			_color = new Color (r, g, b, a);
			branchButtons [i].color = _color;
		}
		//ACTIVE physical branch
		branches [index].SetActive (true);
		branchSR = branches [index].GetComponentInChildren<SpriteRenderer> ();
		//ACTIVE branch ui
		r = g = b = a = 255f / 255f;
		_color = new Color (r, g, b, a);
		branchButtons [index].color = _color;
		AlphaTransition (0.5f);
	}
	public void AlphaTransition (float alpha) {
		branchSR.color = new Color (alpha, alpha, alpha, alpha);

	}

	//
	//COLLISIONS
	//
	void OnTriggerEnter2D (Collider2D other) {

		if (other.tag == "Node") {
			NP = other.GetComponent<NodePrefab> ();
			lockedOnNode = true;
			branchSR = other.gameObject.GetComponent<SpriteRenderer> ();
			moveSpeed = 0;
			transform.position = new Vector3 (other.transform.position.x, other.transform.position.y, -9f);
			branchSpawnLoc.position = other.gameObject.transform.GetChild (0).position;
		}
	}
	void OnTriggerExit2D (Collider2D other) {
		if (other.tag == "Node") {
			moveSpeed = 3;
			lockedOnNode = false;
		}
	}
}
