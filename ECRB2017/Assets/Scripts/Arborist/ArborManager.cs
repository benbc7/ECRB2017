using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArborManager : MonoBehaviour {
    public GameObject[] branchPrefab;
    public Transform branchSpawnLoc;
    public int branchIndex;
    public int maxBranchIndex;
    public Text branchIndexTxt;
	// Use this for initialization
	void Start () {
		maxBranchIndex = branchPrefab.Length;
	}
	
	// Update is called once per frame
	void Update ()
    {
    if (Input.GetKeyDown(KeyCode.G))
        {
            if (branchIndex == 0)
            {
                branchIndex = branchPrefab.Length;
            }
            else
            {
                branchIndex--;
            }
            branchIndexTxt.text = "" + branchIndex;
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (branchIndex == branchPrefab.Length)
            {
                branchIndex = 0;
            }
            else
            {
                branchIndex++;
            }
            branchIndexTxt.text = "" + branchIndex;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(branchPrefab[branchIndex], branchSpawnLoc.position, branchSpawnLoc.rotation);
        }
	}
}
