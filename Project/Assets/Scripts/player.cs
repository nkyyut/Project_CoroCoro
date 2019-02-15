using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour {
    public int hitPoint;
    public int maxHitPoint;
    private Quaternion inputQ;
    // Use this for initialization
    void Start()
    {
        Input.gyro.enabled = true;

    }
	
	// Update is called once per frame
	void Update () {
        inputQ = Input.gyro.attitude;
        Debug.Log("X;"+inputQ.x);
	}
}