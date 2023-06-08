using UnityEngine;

public interface IMovable
{
    void Move(Vector3 direction);
    void Jump();
    void RotateX(float mouseDeltaX);
    void RotateY(float mouseDeltaX, float mouseDeltaY);
}
