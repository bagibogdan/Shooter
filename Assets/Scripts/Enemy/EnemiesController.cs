using Configs;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemiesController : MonoBehaviour
    {
        private EnemiesControllerConfig _enemiesConfig;
        [Inject]
        public void Construct(EnemiesControllerConfig enemiesConfig)
        {
            _enemiesConfig = enemiesConfig;
        }
    }
}