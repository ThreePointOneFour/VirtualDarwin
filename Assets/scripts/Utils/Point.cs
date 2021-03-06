﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KitchenSink
{
    public class Point
    {

        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
        }
    }
}
