using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Player : BaseObject {
    public override bool CanMove(Direction inDirection)
    {
        BaseObject targetObject = new BaseObject();
        if (!this.around.TryGetValue(inDirection, out targetObject)) throw new System.Exception("Can't find key.");

        if (targetObject == null) return true;

        return false;
    }
}
