using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BranchMovement : MonoBehaviour {
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
    public Transform platRotPos;
    public BoxCollider2D plat;
   
    public float rotAngle;
	// Use this for initialization
	void Start ()
    {
        AM = GameObject.Find("ArborManager").GetComponent<ArborManager>();
        UpdateBranchSprite(branchIndex);
        maxBranchIndex = branches.Length - 1;
        AlphaTransition(0.7f);
        

	}

    // Update is called once per frame
    void Update()
    {
        if (facingRight)
            rotAngle = plat.transform.rotation.z;
        else
            rotAngle = plat.transform.rotation.z * -1;
        if (isSnapped)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isLocked = true;

                AlphaTransition(1f);
                AM.branchSpawnLoc.position = new Vector2(0, AM.currentBranch.transform.position.y + 5);
                AM.currentBranch = null;
                Destroy(this);
            }
            else if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isSnapped = false;

                AlphaTransition(0.7f);
            }
        }

        if (!isLocked)
        {
            if (!isSnapped)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.Translate(Vector2.up * (moveSpeed / 100), Space.World);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    transform.Translate(Vector2.down * (moveSpeed / 100), Space.World);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.Translate(Vector2.left * (moveSpeed / 100), Space.World);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Translate(Vector2.right * (moveSpeed / 100), Space.World);
                }
            }


            if (Input.GetKey(KeyCode.Q))
            {
                if (facingRight)
                    platRotPos.transform.Rotate(Vector3.forward * rotSpeed);
                else if (!facingRight)
                    platRotPos.transform.Rotate(Vector3.forward * rotSpeed);
            }
            if (Input.GetKey(KeyCode.E))
            {
                if (facingRight)
                    platRotPos.transform.Rotate(Vector3.back * rotSpeed);
                else if (!facingRight)
                    platRotPos.transform.Rotate(Vector3.back * rotSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && !isSnapped)
            {
                Flip();
            }

            //changes the sprite GO out depending on selection
            if (Input.GetKeyDown(KeyCode.G))
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
            if (Input.GetKeyDown(KeyCode.T))
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
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    public void AlphaTransition (float alpha)
    {
            sr.color = new Color (255, 255, 255, alpha);
        
    }
    void UpdateBranchSprite(int index)
    {
        for (int i = 0; i <= maxBranchIndex; i++)
            branches[i].SetActive(false);

        branches[index].SetActive(true);
        AM.UpdateIndexUI(index);
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
