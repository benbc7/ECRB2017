﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArboristHeadBob : MonoBehaviour {

    public bool isHand;
    public Transform origin;
    private Vector3 midpoint;
    public Transform cursor;
    public float distance;
    public bool isClose;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    // Use this for initialization
    void Start ()
    {
        origin.position = new Vector3(transform.position.x, transform.position.y, (isHand)?1.9f:2f);
	}
	
	// Update is called once per frame
	void Update ()
    {
        midpoint = (origin.position + cursor.position) / 2;
        if (isClose)
            midpoint = (midpoint + cursor.position) / 2;
            midpoint.z = (isHand) ? 1.9f : 2f;
        if (origin.position.x > 0 && cursor.position.x > 0 || origin.position.x <= 0 && cursor.position.x <= 0 && isHand)
        {
            transform.position = Vector3.SmoothDamp(transform.position, midpoint, ref velocity, smoothTime);
        }
        else if (isHand)
        {
            transform.position = Vector3.SmoothDamp(transform.position, origin.position, ref velocity, smoothTime);
        }
       
            midpoint = (origin.position + cursor.position) / 2;
            if (isClose)
            midpoint = (midpoint + cursor.position) / 2;
            midpoint.z = (isHand) ? 1.9f : 2f;
            transform.position = Vector3.SmoothDamp(transform.position, midpoint, ref velocity, smoothTime);

        
    }
}