using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : IInputHandler
{
    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private float minSwipeDistance = 50f;

    public SwipeDirection GetSwipeDirection()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                touchEndPos = touch.position;
                Vector2 swipeVector = touchEndPos - touchStartPos;

                if (swipeVector.magnitude >= minSwipeDistance)
                {
                    float angle = Vector2.Angle(Vector2.up, swipeVector);

                    if (angle < 45)
                        return SwipeDirection.Up;
                    else if (angle >= 45 && angle < 135)
                    {
                        if (swipeVector.x < 0)
                            return SwipeDirection.Left;
                        else
                            return SwipeDirection.Right;
                    }
                    else
                        return SwipeDirection.Down;
                }
            }
        }

        return SwipeDirection.None;
    }
}
