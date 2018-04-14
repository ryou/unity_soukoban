using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Block : BaseObject {
    public override bool CanMove(Direction inDirection)
    {
        BaseObject targetObject;
        this.around.TryGetValue(inDirection, out targetObject);

        return (targetObject == null) ? true : false;
    }
}
