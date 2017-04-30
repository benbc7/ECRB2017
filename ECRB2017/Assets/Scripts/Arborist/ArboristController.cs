using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Rewired;
public class ArboristController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Image[] branchButtons;
    public float moveSpeed;
    public bool canSpawn = false;
    public GameObject masterBranch;
    public GameObject currentBranch;
    public BranchMovement BM;
    public nodeManager NM;
    public Transform branchSpawnLoc;
    public int branchIndex;
    public int maxBranchIndex;
    public Camera cam;
    public float joystickDeadZone;
    public bool fistDrop;
    public float fistTimer;
    public Sprite leftFist;
    public Sprite leftHand;
    public Sprite rightFist;
    public Sprite rightHand;
    public SpriteRenderer LHSR;
    public SpriteRenderer RHSR;

    Vector2 directionalInput;
    Player joystick;
    // Use this for initialization
    void Start()
    {
        joystick = ReInput.players.GetPlayer(0);

        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        maxBranchIndex = branchButtons.Length - 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        directionalInput = new Vector2(joystick.GetAxisRaw("CursorHorizontal"), joystick.GetAxisRaw("CursorVertical"));
        Vector3 targetPos = new Vector3();
        if (directionalInput.x <= joystickDeadZone && directionalInput.x >= -joystickDeadZone)
            rb.AddForce(Vector2.right * directionalInput.x * moveSpeed * Time.deltaTime);
        if (directionalInput.y <= joystickDeadZone && directionalInput.y >= -joystickDeadZone)
            rb.AddForce(Vector2.up * directionalInput.y * moveSpeed * Time.deltaTime);



        targetPos.x += directionalInput.x * moveSpeed * Time.deltaTime;
        targetPos.y += directionalInput.y * moveSpeed * Time.deltaTime;
        

        transform.position += targetPos;

        //Debug.Log("" + targetPos);
    }
    void Update()
    {
        if (fistDrop == true)
        {
            fistTimer -= Time.deltaTime;
            if (fistTimer <= 0)
            {
                fistTimer = 0;
                fistDrop = false;
                UpdateHands(leftHand, rightHand);
              
            }
        }
        if (joystick.GetButtonDown("PlaceBranch") && currentBranch == null && canSpawn == true)
        {

            GameObject B = Instantiate(masterBranch, branchSpawnLoc.transform.position, branchSpawnLoc.transform.rotation);
            currentBranch = B;
            BM = currentBranch.GetComponent<BranchMovement>();
            BM.GetComponent<Rigidbody2D>().isKinematic = true;
            if (NM.rightSide == false)
            {
                BM.Flip();
            }
            UpdateBranchButtons(BM.branchIndex);
        }
    }

    public void UpdateBranchButtons(int ind)
    {
        Color col;
        float r, g, b, a;

        for (int i = 0; i <= maxBranchIndex; i++)
        {
            r = g = b = 255f / 255f;
            a = 50f / 255f;
            col = new Color(r, g, b, a);
            branchButtons[i].color = col;


        }

        r = g = b = a = 255f / 255f;
        col = new Color(r, g, b, a);
        branchButtons[ind].color = col;
    }
    public void UpdateHands(Sprite _leftHand, Sprite _rightHand)
    {
        LHSR.sprite = _leftHand;
        RHSR.sprite = _rightHand;
    }
    void OnTriggerStay2D(Collider2D other)
    {

        if (other.tag == "Node")
        {
            NM = other.GetComponent<nodeManager>();
            canSpawn = true;
            branchSpawnLoc.position = NM.pivot.transform.position;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Node")
        {
            canSpawn = false;
        }
    }

}