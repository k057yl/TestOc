using UnityEngine;

public class InputController
{
    private NewInput _newInput;

    private bool _isCrouchButtonPressed = false;//
    
    public InputController()
    {
        _newInput = new NewInput();
        _newInput.Enable();
    }

    public Vector3 GetMovementInput()
    {
        return _newInput.Gameplay.Movement.ReadValue<Vector3>();
    }
    
    public float GetMouseDeltaInputX()
    {
        return _newInput.Gameplay.DeltaX.ReadValue<float>();
    }
    
    public float GetMouseDeltaInputY()
    {
        return _newInput.Gameplay.DeltaY.ReadValue<float>();
    }

    public float GetMouseButtonMiddle()
    {
        return _newInput.Gameplay.MouseClickMiddleButton.ReadValue<float>();
    }
    
    public bool GetMouseLeft()
    {
        return _newInput.Gameplay.MouseClickLeftButton.triggered;
    }
    
    public bool IsJumpTriggered()
    {
        return _newInput.Gameplay.Jump.triggered;
    }

    public bool GetReloaded()
    {
        return _newInput.Gameplay.Reloaded.triggered;
    }
    public bool IsCrouchButtonPressed()
    {
        bool isPressed = _newInput.Gameplay.MouseClickRihgtButton.ReadValue<float>() != 0f;

        if (isPressed)
        {
            if (!_isCrouchButtonPressed)
            {
                _isCrouchButtonPressed = true;
                return true;
            }
        }
        else
        {
            _isCrouchButtonPressed = false;
        }

        return false;
    }
}
