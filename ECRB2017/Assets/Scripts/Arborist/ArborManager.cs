using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArborManager : MonoBehaviour {
    public Image[] branchButtons;

    public bool canSpawn = false;
    public GameObject masterBranch;
    public GameObject currentBranch;
    public BranchMovement BM;
    public nodeManager NM;
    public Transform branchSpawnLoc;
    public int branchIndex;
    public int maxBranchIndex;
    public Text branchIndexTxt;
    public Camera cam;
	// Use this for initialization
	void Start () {
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        maxBranchIndex = branchButtons.Length - 1;
    }
	
	// Update is called once per frame
	void Update ()
    {

        Vector3 p = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.nearClipPlane));
        transform.position = new Vector3(p.x, p.y, -1);
        if (Input.GetKeyUp(KeyCode.Mouse0) && currentBranch == null && canSpawn == true)
        {
            GameObject B = Instantiate(masterBranch, branchSpawnLoc.transform.position, branchSpawnLoc.transform.rotation);
            currentBranch = B;

            BM = currentBranch.GetComponent<BranchMovement>();
            if (NM.leftSide == true)
            {
                BM.Flip();
            }
            UpdateBranchButtons(BM.branchIndex);
        }
	}
    public void UpdateIndexUI(int index)
    {
        branchIndexTxt.text = "" + index;
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
    void OnTriggerEnter2D(Collider2D other)
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