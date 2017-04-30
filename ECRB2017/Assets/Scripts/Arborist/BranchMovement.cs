using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Rewired;
public class BranchMovement : MonoBehaviour
{
    public float moveSpeed;
    public float rotSpeed;
    public bool isLocked;
    public bool isSnapped;
    public bool facingRight;

    public int branchIndex = 1;
    public int maxBranchIndex;
    public GameObject[] branches;
    public SpriteRenderer sr;
    public ArboristController AC;
    public BoxCollider2D plat;
    public Transform platRotPos;


    public float joystickDeadZone;
    Vector2 directionalInput;
    Player joystick;
    public float rotAngle;
    // Use this for initialization
    void Start()
    {
        joystick = ReInput.players.GetPlayer(0);
        AC = GameObject.Find("ArboristController").GetComponent<ArboristController>();
        maxBranchIndex = branches.Length - 1;
        branchIndex = Random.Range(0, maxBranchIndex);
        UpdateBranchSprite(branchIndex);
        AC.UpdateBranchButtons(branchIndex);
        AlphaTransition(0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        directionalInput = new Vector2(joystick.GetAxisRaw("CursorHorizontal"), joystick.GetAxisRaw("CursorVertical"));
        if (facingRight)
            rotAngle = plat.transform.rotation.z * -1;
        else
            rotAngle = plat.transform.rotation.z;

        if (joystick.GetButtonDown("PlaceBranch") && isLocked == false)
        {
           
            isLocked = true;
            AlphaTransition(1f);
            branches[branchIndex].transform.GetChild(0).gameObject.SetActive(true);
            AC.currentBranch = null;
            AC.fistDrop = true;
            AC.fistTimer = 0.5f;
            if (AC.gameObject.transform.position.x >= 0)
                AC.UpdateHands(AC.leftHand, AC.rightFist);
            else
                AC.UpdateHands(AC.leftFist, AC.rightHand);
        }
        
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Destroy(gameObject);
            plat = null;
            AC.currentBranch = null;
            AlphaTransition(0.5f);
        }


        if (!isLocked)
        {



            if (rotAngle <= 0.15 && directionalInput.y >= joystickDeadZone && directionalInput.y <= -joystickDeadZone)
            {
                if (facingRight)
                    platRotPos.transform.Rotate(Vector3.forward * directionalInput.y * rotSpeed);
                else if (!facingRight)
                    platRotPos.transform.Rotate(Vector3.forward * directionalInput.y * rotSpeed);
            }
            if (rotAngle >= 0.15 && directionalInput.y <= joystickDeadZone && directionalInput.y >= -joystickDeadZone)
            {
                if (facingRight)
                    platRotPos.transform.Rotate(Vector3.forward * directionalInput.y * rotSpeed);
                else if (!facingRight)
                    platRotPos.transform.Rotate(Vector3.forward * directionalInput.y * rotSpeed);
            }
            if (Input.GetKey(KeyCode.S) && rotAngle >= -0.15)
            {
                if (facingRight)
                    platRotPos.transform.Rotate(Vector3.back * rotSpeed);
                else if (!facingRight)
                    platRotPos.transform.Rotate(Vector3.back * rotSpeed);
            }

            //changes the sprite GO out depending on selection
            if (joystick.GetButtonDown("PreviousBranch"))
            {
                if (branchIndex == 0)
                {
                    branchIndex = maxBranchIndex;
                }

                else
                {
                    branchIndex--;
                }
                UpdateBranchSprite(branchIndex);
                AC.UpdateBranchButtons(branchIndex);
            }
            if (joystick.GetButtonDown("NextBranch"))
            {
                if (branchIndex == maxBranchIndex)
                {
                    branchIndex = 0;

                }
                else
                {
                    branchIndex++;
                }
                UpdateBranchSprite(branchIndex);
                AC.UpdateBranchButtons(branchIndex);
            }
        }
    }
    public void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void AlphaTransition(float alpha)
    {
        sr.color = new Color(alpha, alpha, alpha, alpha);

    }
    void UpdateBranchSprite(int index)

    {
        for (int i = 0; i <= maxBranchIndex; i++)
        {
            branches[i].SetActive(false);
            branches[i].GetComponent<SpriteRenderer>();
            AlphaTransition(1);
        }
        branches[index].SetActive(true);
        platRotPos = branches[index].GetComponentInChildren<Transform>();
        plat = branches[index].GetComponentInChildren<BoxCollider2D>();
        sr = branches[index].GetComponentInChildren<SpriteRenderer>();

        AlphaTransition(0.5f);
    }

    /*
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("trig enter");
        if (other.tag == "Node") 
        {
            if (other.GetComponent<nodeManager>().isOccupied == false)
            {
                if (facingRight && other.gameObject.GetComponent<nodeManager>().rightSide == true)
                {
                    transform.position = other.transform.position;
                    isSnapped = true;
                }
                else if (!facingRight && other.gameObject.GetComponent<nodeManager>().leftSide)
                {
                    transform.position = other.transform.position;
                    isSnapped = true;
                }
            }
        }
    }
    */

}
