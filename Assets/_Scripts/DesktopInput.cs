using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopInput : IInput
{
    public event Action<Vector2> OnMove;

    public void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

        inputVector.Normalize();

        OnMove?.Invoke(inputVector);
    }
}
