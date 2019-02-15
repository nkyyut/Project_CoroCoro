using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCount : MonoBehaviour {


    private Vector3 Acceleration;
    private Vector3 preAcceleration;
    float DotProduct;
    public static float Count;

    //public static bool Shake = false;
    // Use this for initialization

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        preAcceleration = Acceleration;
        Acceleration = Input.acceleration;
        DotProduct = Vector3.Dot(Acceleration, preAcceleration);
        if (DotProduct < 0)
        {
            //Shake = true;
            Count++;
            //Debug.Log(Count);
        }
    }
}
