using UnityEngine;

[CreateAssetMenu(fileName = "CharacterStatsConfig",menuName = "Configs")]
public class CharacterConfig : ScriptableObject
{
    public float Gravity = -9.81f;
    public float JumpHeight;
    public float Speed;
    public LayerMask GroundMask;
    public float GroundCheckRadius;
    public float Sensivity;
}
