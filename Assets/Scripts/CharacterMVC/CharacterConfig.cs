using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsConfig",menuName = "Configs")]
public class CharacterConfig : ScriptableObject
{
    public float Gravity = Constants.GRAVITY;
    public float JumpHeight;
    public float Speed;
    public LayerMask GroundMask;
    public float GroundCheckRadius;
    public float Sensivity;
}
