using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Player : BaseObject {
    public override void Move(Direction inDirection)
    {
        BaseObject targetObject;
        if (!this.around.TryGetValue(inDirection, out targetObject)) throw new System.Exception("Can't find key.");

        base.Move(inDirection);
        if (targetObject != null && targetObject.CanMove(inDirection)) targetObject.Move(inDirection);
    }

    public override bool CanMove(Direction inDirection)
    {
        BaseObject targetObject;
        if (!this.around.TryGetValue(inDirection, out targetObject)) throw new System.Exception("Can't find key.");

        if (targetObject == null) return true;
        if (targetObject.CanMove(inDirection)) return true;

        return false;
    }
}
