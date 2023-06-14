using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _anim;

    public void PlayWalkAnimation(Vector3 direction)
    {
        _anim.SetFloat("Speed", direction.magnitude);
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
}