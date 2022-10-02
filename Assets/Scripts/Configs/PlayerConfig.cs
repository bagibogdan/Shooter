using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private float movementSpeed = 20f;
        
        public float MovementSpeed => movementSpeed;
    }
}