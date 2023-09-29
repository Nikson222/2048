using System;
using UnityEngine;

public class DesktopInput : IInputHandler
{
    public SwipeDirection GetSwipeDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 inputVector = new Vector2(horizontalInput, verticalInput);

        inputVector.Normalize();

        if (inputVector.magnitude > 0)
        {
            float angle = Vector2.SignedAngle(Vector2.up, inputVector);

            if (angle >= -45 && angle < 45)
                return SwipeDirection.Up;
            else if (angle >= 45 && angle < 135)
                return SwipeDirection.Left;
            else if (angle >= -135 && angle < -45)
                return SwipeDirection.Right;
            else
                return SwipeDirection.Down;
        }

        return SwipeDirection.None;
    }
}
