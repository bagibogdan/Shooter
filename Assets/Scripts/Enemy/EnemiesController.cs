using System.Collections.Generic;
using Configs;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemiesController : MonoBehaviour
    {
        private const int DEFAULT_ENEMIES_COUNT = 3;
        private const int ENEMIES_INCREASE_LEVEL = 3;

        private List<GameObject> _enemiesList = new List<GameObject>();
        private EnemiesControllerConfig _enemiesConfig;
        private Vector3 _position;
        private Vector3 _currentEnemyPosition;
        private int _currentEnemiesCount;
        private RedEnemyController.Factory _redFactory;
        private WhiteEnemyController.Factory _whiteFactory;
        private YellowEnemyController.Factory _yellowFactory;
        
        [Inject]
        public void Construct(EnemiesControllerConfig enemiesConfig, 
            RedEnemyController.Factory redFactory,
            WhiteEnemyController.Factory whiteFactory,
            YellowEnemyController.Factory yellowFactory)
        {
            _enemiesConfig = enemiesConfig;
            _redFactory = redFactory;
            _yellowFactory = yellowFactory;
            _whiteFactory = whiteFactory;
        }

        private void Awake()
        {
            _currentEnemiesCount = DEFAULT_ENEMIES_COUNT;
            _position = transform.position;
            _enemiesList.Add(_enemiesConfig.RedEnemy);
            _enemiesList.Add(_enemiesConfig.WhiteEnemy);
            _enemiesList.Add(_enemiesConfig.YellowEnemy);
        }

        public void CreateEnemies(int levelIndex)
        {
            if (levelIndex % ENEMIES_INCREASE_LEVEL == 0)
            {
                _currentEnemiesCount = DEFAULT_ENEMIES_COUNT + (int)(levelIndex / ENEMIES_INCREASE_LEVEL);
            }

            for (int i = 0; i < _currentEnemiesCount; i++)
            {
                _currentEnemyPosition = new Vector3(UnityEngine.Random.Range(-3f, 3f), _position.y, _position.z);
                EnemyController enemy;
                
                if (i == 0) enemy = _redFactory.Create(ScriptableObject.CreateInstance<RedEnemyConfig>());
                else if (i % 2 == 0) enemy = _whiteFactory.Create(ScriptableObject.CreateInstance<WhiteEnemyConfig>());
                else if (i % 2 != 0) enemy = _yellowFactory.Create(ScriptableObject.CreateInstance<YellowEnemyConfig>());
                else enemy = _yellowFactory.Create(ScriptableObject.CreateInstance<YellowEnemyConfig>());

                enemy.gameObject.transform.position = _currentEnemyPosition;
            }
        }
        
        public void ClearEnemies()
        {
            var clearedObjects = new List<EnemyController>();
            
            clearedObjects.AddRange(FindObjectsOfType<EnemyController>());

            foreach (var enemy in clearedObjects)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}