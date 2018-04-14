using System.Collections;
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

    public class Position
    {
        public int x = 0;
        public int y = 0;

        public Position()
        {

        }

        public Position(int inX, int inY)
        {
            this.x = inX;
            this.y = inY;
        }
    }
}
