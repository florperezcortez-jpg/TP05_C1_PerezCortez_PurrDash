
    using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PurrDash/Player Data")]
public class PlayerData : ScriptableObject
{
    public float speed = 5f;
    public float jumpForce = 10f;
    public int lives = 3;
}
