using UnityEngine;

public interface IMovable
{
    void Move(Vector3 direction);
    void Jump();
    void Rotate(float mouseDeltaX, float mouseDeltaY);
}
