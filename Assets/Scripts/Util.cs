﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public enum Direction
    {
        none = -1,
        up = 0,
        right,
        down,
        left
    }

    public struct Position
    {
        public int x;
        public int y;
    }
}
