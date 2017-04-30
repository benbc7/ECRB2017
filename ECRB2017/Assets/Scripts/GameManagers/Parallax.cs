using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float scrollfactor;
    public Transform camPos;
    public float zDepth;
    
    void Update()
    {
        transform.position = new Vector3(0, camPos.position.y * scrollfactor, zDepth);
    }
	
}
