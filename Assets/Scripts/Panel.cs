using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Panel : MonoBehaviour {

    private Position position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPosition(Position inPosition)
    {
        this.position = inPosition;
        this.transform.Translate(inPosition.x, 0, inPosition.y);
    }
}
