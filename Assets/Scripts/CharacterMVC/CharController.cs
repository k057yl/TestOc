using System;
using UnityEngine;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterConfig _characterConfig;
    [SerializeField] private Transform _characterCamera;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private GameObject _soundManagerPrefab;
    [SerializeField] private Transform _pistolShootPoint;
    [SerializeField] private GameObject _uiPrefab;
    
    
    private InputController _inputController;
    private SoundManager _soundManager;
    private CharacterModel _characterModel;
    public CharacterModel CharacterModel => _characterModel;
    private CharacterView _characterView;
    private Weapon _pistol;
    private UIBar _uiBar;
    

    private Vector3 _direction;
    private float _xRotation;
    private float _velocity;
    private float _deltaX;
    private float _deltaY;
    private bool _onGround = false;
    private bool _isCrouching = false;


    private void Start()
    {
        InitializeComponents();
        LockCursor();
        
        _characterModel.KilledChanged += OnKilledChanged;
        _characterModel.HealthChanged += OnHealthText;
    }
    
    private void OnDisable()
    {
        _characterModel.KilledChanged -= OnKilledChanged;
        _characterModel.HealthChanged -= OnHealthText;
    }
    
    private void Initialization(UIBar uiBar)
    {
        _uiBar = uiBar;
    }
    
    private void InitializeComponents()
    {
        _inputController = new InputController();
        _characterModel = new CharacterModel();

        _characterView = GetComponent<CharacterView>();

        GameObject soundManagerObject = Instantiate(_soundManagerPrefab);
        _soundManager = soundManagerObject.GetComponent<SoundManager>();

        _uiBar = Instantiate(_uiPrefab).GetComponent<UIBar>();
        Initialization(_uiBar);

        _pistol = new Weapon();
        _pistol.SetStrategy(new Pistol(_pistolShootPoint, _uiBar));
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        Move();
        Rotate();
    }

    private void Update()
    {
        HandleInput();
    }

    private void Move()
    {
        DoGravity();
        
        _direction = _inputController.GetMovementInput();
        var characterTransform = _characterController.transform;
        _direction = characterTransform.right * _direction.x + characterTransform.forward * _direction.z;
        
        _characterController.Move(_direction * (_characterConfig.Speed * Time.deltaTime));
        _characterView.PlayWalkAnimation(_direction);
        _characterView.PlayWalkLeft(_direction);
    }

    private void Rotate()
    {
        _deltaX = _inputController.GetMouseDeltaInputX();
        _deltaY = _inputController.GetMouseDeltaInputY();
        
        float sensitivityX = _characterConfig.Sensivity;
        float sensitivityY = _characterConfig.Sensivity;

        _xRotation -= _deltaY * sensitivityY;
        _xRotation = Mathf.Clamp(_xRotation, Constants.NEGATIVE_LIMIT, Constants.POSITIVE_LIMIT);

        _characterCamera.localRotation = Quaternion.Euler(_xRotation, Constants.ZERO, Constants.ZERO);
        _characterController.transform.Rotate(Vector3.up * (_deltaX * sensitivityX));
    }
    
    private void DoGravity()
    {
        _velocity += _characterConfig.Gravity * Time.fixedDeltaTime;
        
        _characterController.Move(Vector3.up * (_velocity * Time.fixedDeltaTime));
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
        
        if (_inputController.GetExit())
        {
            ExitGame();
        }
    }
    
    private void ToggleCrouch()
    {
        _isCrouching = !_isCrouching;

        if (_isCrouching)
        {
            _characterView.PlayShootAnimation(Constants.ZERO);
        }
        else
        {
            _characterView.PlayShootAnimation(Constants.ONE);
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
    
    private bool IsGround()
    {
        bool result = Physics.CheckSphere(
            _groundChecker.position,
            _characterConfig.GroundCheckRadius,
            _characterConfig.GroundMask);
        return result;
    }
    
    private void Jump()
    {
        _onGround = IsGround();
        if(_onGround)
        {
            _characterView.PlayJumpTrigger(_onGround);
            _velocity = Mathf.Sqrt(_characterConfig.JumpHeight * Constants.VELOCITY_DECREASEMENT * _characterConfig.Gravity);
        }
    }
    
    private void OnKilledChanged(int killed)
    {
        _uiBar.UpdateKilledText(killed);
    }
    
    private void OnHealthText(int health)
    {
        _uiBar.UpdateHealthText(health);
    }

    private void ExitGame()
    {
        Action action = () => {
            Application.Quit();
        };

        Popup popup = PopupController.Instance.CreatePopup();
        PopupController.Instance.Canvas = _uiBar.transform;
        popup.Init(PopupController.Instance.Canvas,
            "Inspector Cat",
            "Are you sure you want to quit?",
            "Stay here",
            action
        );
    }
}