using System;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private Transform _characterCamera;
    [SerializeField] private Transform _groundChecker;
    
    private NewInput _newInput;
    private CharacterModel _characterModel;
    
    private void Start()
    {
        _newInput = new NewInput();
        _newInput.Enable();

        _characterModel = new CharacterModel(_characterController, _characterConfig, _characterCamera, _groundChecker);
    }

    private void FixedUpdate()
    {
        Vector3 direction = _newInput.Gameplay.Movement.ReadValue<Vector3>();
        float mouseDeltaX = _newInput.Gameplay.DeltaX.ReadValue<float>();
        float mouseDeltaY = _newInput.Gameplay.DeltaY.ReadValue<float>();
        
        _characterModel.Move(direction);
        _characterModel.RotateX(mouseDeltaX);

        if (_newInput.Gameplay.MouseClickRihgtButton.ReadValue<float>() != 0)
        {
            _characterModel.RotateY(mouseDeltaX, mouseDeltaY);
        }
    }

    private void Update()
    {
        if (_newInput.Gameplay.Jump.triggered)
        {
            _characterModel.Jump();
        }
    }
}
