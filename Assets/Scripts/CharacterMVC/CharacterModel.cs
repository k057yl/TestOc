using UnityEngine;

public class CharacterModel : IMovable
{
    private const int VELOCITY_DECREASEMENT = -2;
    
    private CharacterController _characterController;
    private CharacterConfig _characterConfig;

    private Transform _characterCamera;
    private Transform _groundChecker;
    
    private float _xRotation;
    private float _velocity;
    
    public CharacterModel(CharacterController characterController, CharacterConfig characterConfig, Transform characterCamera, Transform groundChecker)
    {
        _characterController = characterController;
        _characterConfig = characterConfig;
        _characterCamera = characterCamera;
        _groundChecker = groundChecker;
    }
    
    public void Move(Vector3 direction)
    {
        DoGravity();
        
        var characterTransform = _characterController.transform;
        direction = characterTransform.right * direction.x + characterTransform.forward * direction.z;
        
        _characterController.Move(direction * (_characterConfig.Speed * Time.deltaTime));
    }

    public void RotateX(float mouseDeltaX)
    {
        _characterController.transform.Rotate(Vector3.up * (mouseDeltaX * _characterConfig.Sensivity * Time.deltaTime));
    }

    public void RotateY(float mouseDeltaX, float mouseDeltaY)
    {
        _xRotation  -= mouseDeltaY;

        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        _characterCamera.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _characterController.transform.Rotate(Vector3.up * mouseDeltaX);
    }
    
    public void DoGravity()
    {
        _velocity += _characterConfig.Gravity * Time.fixedDeltaTime;
        
        _characterController.Move(Vector3.up * (_velocity * Time.fixedDeltaTime));
    }

    public bool IsGround()
    {
        bool result = Physics.CheckSphere(
            _groundChecker.position,
            _characterConfig.GroundCheckRadius,
            _characterConfig.GroundMask);
        return result;
    }
    
    public void Jump()
    {
        bool isGrounded = IsGround();
        if (isGrounded)
        {
            _velocity = Mathf.Sqrt(_characterConfig.JumpHeight * VELOCITY_DECREASEMENT * _characterConfig.Gravity);
        }
    }
}
