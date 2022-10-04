using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float movementSpeed = 20f;
        [SerializeField] private int maxHealth = 300;

        public float MovementSpeed => movementSpeed;
        public int MaxHealth => maxHealth;
    }
}