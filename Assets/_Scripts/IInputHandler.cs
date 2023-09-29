using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputHandler
{
    SwipeDirection GetSwipeDirection();
}

public enum SwipeDirection
{
    Up,
    Down,
    Left,
    Right,
    None
}
