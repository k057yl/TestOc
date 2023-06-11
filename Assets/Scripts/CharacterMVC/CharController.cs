using System;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private Transform _characterCamera;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private GameObject _soundManagerPrefab;
    [SerializeField] private Transform _pistolShootPoint;
    

    private InputController _inputController;
    private SoundManager _soundManager;

    private CharacterModel _characterModel;
    private CharacterView _characterView;
    private Weapon _pistol;
   

    private bool _isCrouching = false;

    private void Start()
    {
        InitializeComponents();
        LockCursor();
    }

    private void Update()
    {
        HandleInput();
    }

    private void FixedUpdate()
    {
        Move();
        RotateCharacter();
    }

    private void InitializeComponents()
    {
        _inputController = new InputController();

        _characterModel = new CharacterModel(_characterController, _characterConfig, _characterCamera, _groundChecker);
        _characterView = GetComponent<CharacterView>();

        GameObject soundManagerObject = Instantiate(_soundManagerPrefab);
        _soundManager = soundManagerObject.GetComponent<SoundManager>();

        UIBar uiBar = FindObjectOfType<UIBar>();
        
        _pistol = new Weapon();
        _pistol.SetStrategy(new Pistol(_pistolShootPoint, uiBar));
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void HandleInput()
    {
        if (_inputController.IsJumpTriggered())
        {
            Jump();
        }

        if (_inputController.IsCrouchButtonPressed())
        {
            ToggleCrouch();
        }

        if (_inputController.GetMouseLeft())
        {
            FireWeapon();
        }

        if (_inputController.GetReloaded())
        {
            ReloadWeapon();
        }
    }

    private void Move()
    {
        _characterModel.Move(_inputController.GetMovementInput());
        _characterView.SetSpeed(_inputController.GetMovementInput().magnitude);

        if (_inputController.GetMovementInput().magnitude > 0)
        {
            _soundManager.PlayStepSound();
        }
    }

    private void RotateCharacter()
    {
        _characterModel.Rotate(_inputController.GetMouseDeltaInputX(), _inputController.GetMouseDeltaInputY());
    }

    private void Jump()
    {
        _characterView.SetJumpTrigger();
        _characterModel.Jump();
    }

    private void ToggleCrouch()
    {
        _isCrouching = !_isCrouching;

        if (_isCrouching)
        {
            _characterView.SetShootAnimation(0);
        }
        else
        {
            _characterView.SetShootAnimation(1);
        }
    }

    private void FireWeapon()
    {
        _pistol.Fire(_pistolShootPoint);
        _soundManager.PlayShotSound();
    }

    private void ReloadWeapon()
    {
        _pistol.ReloadByButton();
    }
}