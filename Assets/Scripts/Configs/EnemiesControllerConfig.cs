using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "EnemiesControllerConfig", menuName = "Configs/EnemiesControllerConfig")]
    public class EnemiesControllerConfig : ScriptableObject
    {
        [SerializeField] private GameObject redEnemy;
        [SerializeField] private GameObject whiteEnemy;
        [SerializeField] private GameObject yellowEnemy;

        public GameObject YellowEnemy => yellowEnemy;
        public GameObject WhiteEnemy => whiteEnemy;
        public GameObject RedEnemy => redEnemy;
    }
}