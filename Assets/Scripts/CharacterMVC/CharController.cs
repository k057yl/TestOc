using System;
using UnityEngine;

public class CharController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private Transform _characterCamera;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private GameObject _soundManagerPrefab;

    [SerializeField] private Transform _pistolShootPoint;//

    private InputController _inputController;
    private SoundManager _soundManager;

    private CharacterModel _characterModel;
    private CharacterView _characterView;

    private Weapon _pistol;//

    private bool _isCrouching = false;
    
    

    private void Start()
    {
        _inputController = new InputController();

        _characterModel = new CharacterModel(_characterController, _characterConfig, _characterCamera, _groundChecker);
        _characterView = GetComponent<CharacterView>();

        GameObject soundManagerObject = Instantiate(_soundManagerPrefab);
        _soundManager = soundManagerObject.GetComponent<SoundManager>();

        _pistol = new Weapon();
        _pistol.SetStrategy(new PistolStrategy(_pistolShootPoint));
    }

    private void FixedUpdate()
    {
        Move();
        _characterModel.RotateX(_inputController.GetMouseDeltaInputX());

        if (_inputController.GetMouseButtonMiddle() != 0)
        {
            _characterModel.RotateY(_inputController.GetMouseDeltaInputX(), _inputController.GetMouseDeltaInputY());
        }
    }

    private void Update()
    {
        if (_inputController.IsJumpTriggered())
        {
            _characterView.SetJumpTrigger();
            _characterModel.Jump();
        }

        if (_inputController.IsCrouchButtonPressed())
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

        if (_inputController.GetMouseLeft())
        {
            _pistol.Fire(_pistolShootPoint);
        }

        if (_inputController.GetReloaded())
        {
            _pistol.Reload();
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
}