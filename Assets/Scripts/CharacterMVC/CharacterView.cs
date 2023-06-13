using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public void PlayWalkAnimation(Vector3 direction)
    {
        _anim.SetFloat("DeltaY", direction.z);
    }
    
    public void PlayShootAnimation(float shootValue)
    {
        _anim.SetFloat("Shoot", shootValue);
    }
    
    public void PlayJumpTrigger(bool onGround)
    {
        if (onGround)
        {
            _anim.SetTrigger("Jump");
        }
    }
    
    public void PlayWalkLeft(Vector3 direction)
    {
        if (direction.x > 0)
        {
            _anim.SetFloat("DeltaX", direction.x);
        }
        
        if (direction.x < 0)
        {
            _anim.SetFloat("DeltaX", direction.x);
        }
    }
    
}
/*
public class CharacterView
{
    private Animator _anim;
    //private CharacterModel _characterModel;

    //private Vector3 _direction;
    //private bool _onGround;

    private void Start()
    {
        //_anim = GetComponent<Animator>();
        //_characterModel = new CharacterModel();
        //_characterModel = GetComponent<CharacterModel>();
    }

    private void Update()
    {
        //SetMoveZ();
        //SetJumpTrigger();
    }
    
    public void SetCharacterModel(CharacterModel characterModel)
    {
        _characterModel = characterModel;
    }

    public void SetMoveZ()
    {
        _anim.SetFloat("DeltaY", 1);
        
        if (_characterModel != null)
        {
            
        }
        
    }
    
    public void SetMoveX(float deltaX)
    {
        if (deltaX > 0)
        {
            _anim.SetFloat("DeltaX", 1);
        }
        
        if (deltaX < 0)
        {
            _anim.SetFloat("DeltaX", -1);
        }
    }

    public void SetJumpTrigger()
    {
        
        if (!_characterModel.OnGround)
        {
            _anim.SetTrigger("Jump");
        }
        
    }

    public void SetShootAnimation(float shootValue)
    {
        _anim.SetFloat("Shoot", shootValue);
    }
}
*/