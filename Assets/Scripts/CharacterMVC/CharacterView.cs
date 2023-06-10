using UnityEngine;

public class CharacterView : MonoBehaviour
{
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetSpeed(float speed)
    {
        _anim.SetFloat("Speed", speed);
    }

    public void SetJumpTrigger()
    {
        _anim.SetTrigger("Jump");
    }

    public void SetShootAnimation(float shootValue)
    {
        _anim.SetFloat("Shoot", shootValue);
    }
}