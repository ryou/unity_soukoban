using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour {

    private Vector2 position;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetPosition(Vector2 inPosition)
    {
        this.position = inPosition;
        this.transform.Translate(inPosition.x, 0, inPosition.y);
    }
}
