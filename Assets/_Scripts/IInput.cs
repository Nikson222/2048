using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInput
{
    event Action<Vector2> OnMove;
}
