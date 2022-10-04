using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float movementSpeed = 20f;
        [SerializeField] private int maxHealth = 525;

        public float MovementSpeed => movementSpeed;
        public int MaxHealth => maxHealth;
    }
}