using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class Player : BaseObject {
    public override bool CanMove(Direction inDirection)
    {
        return true;
    }
}
