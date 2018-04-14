using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class BaseObject : MonoBehaviour {

    enum State
    {
        idle = 0,
        move,
    }

    private Vector2 position;
    private Cell[] around = new Cell[4];

    private State currentState = State.idle;
    private Vector3 movePosition;

    private float speed = 4.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (this.currentState == State.move)
        {
            var vector = this.movePosition - this.transform.position;
            var moveDistance = this.speed * Time.deltaTime;
            if (Vector3.Distance(this.movePosition, this.transform.position) > moveDistance)
            {
                this.transform.Translate(vector.normalized * moveDistance);
            }
            else
            {
                this.transform.position = this.movePosition;
                this.currentState = State.idle;
            }
        }
	}

    public void Move(Direction inDirection)
    {
        Vector2 translate;

        switch(inDirection)
        {
            case Direction.up:
                translate = new Vector2(0, 1);
                break;
            case Direction.right:
                translate = new Vector2(1, 0);
                break;
            case Direction.down:
                translate = new Vector2(0, -1);
                break;
            case Direction.left:
                translate = new Vector2(-1, 0);
                break;
            default:
                throw new System.Exception("Invalid Direction");
        }
        
        this.position += translate;
        this.movePosition = new Vector3(this.position.x, 0, this.position.y);
        this.currentState = State.move;
    }

    public virtual bool CanMove(Direction inDirection)
    {
        return false;
    }

    public bool IsMoving()
    {
        return this.currentState == State.move;
    }

    public void SetAround(BaseObject[] inAround)
    {

    }

    public void SetPosition(Vector2 inPosition)
    {
        this.position = inPosition;
        this.transform.Translate(inPosition.x, 0, inPosition.y);
    }
}
