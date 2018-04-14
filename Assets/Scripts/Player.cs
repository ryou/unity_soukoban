using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Player : BaseObject {

    private GameObject model;
    private Animator animator;

    private void Start()
    {
        this.model = this.transform.Find("Model").gameObject;
        this.animator = this.model.GetComponent<Animator>();
    }

    public override void Move(Direction inDirection)
    {
        BaseObject targetObject;
        if (!this.around.TryGetValue(inDirection, out targetObject)) throw new System.Exception("Can't find key.");

        base.Move(inDirection);
        if (targetObject != null && targetObject.CanMove(inDirection)) targetObject.Move(inDirection);

        int angle = 0;
        switch (inDirection)
        {
            case Direction.up:
                angle = 0;
                break;
            case Direction.right:
                angle = 90;
                break;
            case Direction.down:
                angle = 180;
                break;
            case Direction.left:
                angle = 270;
                break;
        }
        this.model.transform.rotation = Quaternion.Euler(0, angle, 0);

        this.animator.SetBool("Walking", true);
    }

    public override bool CanMove(Direction inDirection)
    {
        BaseObject targetObject;
        if (!this.around.TryGetValue(inDirection, out targetObject)) throw new System.Exception("Can't find key.");

        if (targetObject == null) return true;
        if (targetObject.CanMove(inDirection)) return true;

        return false;
    }

    protected override void OnEndMove()
    {
        base.OnEndMove();

        this.animator.SetBool("Walking", false);
    }
}
