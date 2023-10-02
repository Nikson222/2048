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
