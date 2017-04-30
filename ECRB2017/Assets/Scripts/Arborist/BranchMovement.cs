using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public ArborManager AM;
    public BoxCollider2D plat;
    public Transform platRotPos;

    public float rotAngle;
    // Use this for initialization
    void Start()
    {
        AM = GameObject.Find("ArborManager").GetComponent<ArborManager>();
        UpdateBranchSprite(branchIndex);
        maxBranchIndex = branches.Length - 1;
        AlphaTransition(0.5f);


    }

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
            rotAngle = plat.transform.rotation.z * -1;
        else
            rotAngle = plat.transform.rotation.z;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLocked = true;
            AlphaTransition(1f);
            branches[branchIndex].transform.GetChild(0).gameObject.SetActive(true);
            AM.currentBranch = null;
        }
        
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Destroy(gameObject);
            plat = null;
            AM.currentBranch = null;
            AlphaTransition(0.5f);
        }


        if (!isLocked)
        {



            if (Input.GetKey(KeyCode.W) && rotAngle <= 0.15)
            {
                if (facingRight)
                    platRotPos.transform.Rotate(Vector3.forward * rotSpeed);
                else if (!facingRight)
                    platRotPos.transform.Rotate(Vector3.forward * rotSpeed);
            }
            if (Input.GetKey(KeyCode.S) && rotAngle >= -0.15)
            {
                if (facingRight)
                    platRotPos.transform.Rotate(Vector3.back * rotSpeed);
                else if (!facingRight)
                    platRotPos.transform.Rotate(Vector3.back * rotSpeed);
            }

            //changes the sprite GO out depending on selection
            if (Input.GetKeyDown(KeyCode.A))
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
                AM.UpdateBranchButtons(branchIndex);
            }
            if (Input.GetKeyDown(KeyCode.D))
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
                AM.UpdateBranchButtons(branchIndex);


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
        sr.color = new Color(255, 255, 255, alpha);

    }
    void UpdateBranchSprite(int index)
    {
        for (int i = 0; i <= maxBranchIndex; i++)
            branches[i].SetActive(false);

        branches[index].SetActive(true);
        platRotPos = branches[index].GetComponentInChildren<Transform>();
        plat = branches[index].GetComponentInChildren<BoxCollider2D>();
        sr = branches[index].GetComponentInChildren<SpriteRenderer>();
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
