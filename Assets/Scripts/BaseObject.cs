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

    private Position position;
    protected Dictionary<Direction, BaseObject> around;

    private State currentState = State.idle;
    private Vector3 movePosition;

    private float speed = 3.0f;

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
                OnEndMove();
            }
        }
	}

    public virtual void Move(Direction inDirection)
    {
        var translate = new Position();

        switch(inDirection)
        {
            case Direction.up:
                translate.y = 1;
                break;
            case Direction.right:
                translate.x = 1;
                break;
            case Direction.down:
                translate.y = -1;
                break;
            case Direction.left:
                translate.x = -1;
                break;
            default:
                throw new System.Exception("Invalid Direction");
        }

        this.position.x += translate.x;
        this.position.y += translate.y;
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

    public void SetAround(Dictionary<Direction, BaseObject> inAround)
    {
        this.around = inAround;
    }

    public Position GetPosition()
    {
        return this.position;
    }

    public void SetPosition(Position inPosition)
    {
        this.position = inPosition;
        this.transform.Translate(inPosition.x, 0, inPosition.y);
    }

    protected virtual void OnEndMove()
    {
        this.currentState = State.idle;
    }
}
