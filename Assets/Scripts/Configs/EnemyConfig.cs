using UnityEngine;

namespace Configs
{
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private float movementSpeed = 5f;
        [SerializeField] private int maxHealth = 100;

        public float MovementSpeed => movementSpeed;
        public int MaxHealth => maxHealth;
    }
}