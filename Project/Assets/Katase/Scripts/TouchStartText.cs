using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchStartText : MonoBehaviour {

    Text targetText;

	// Use this for initialization
	void Start () {
        targetText = this.GetComponent<Text>();
        enabled = true;
	}

	void Update () {
        	
	}

    // 消す
    public void Enabled()
    {
        enabled = false;
    }
}
