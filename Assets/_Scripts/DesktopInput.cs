using System;
using UnityEngine;

public class DesktopInput : InputHandler
{
    public Vector2 GetMoveDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

        inputVector.Normalize();

        return inputVector;
    }
}
