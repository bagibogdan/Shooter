using Zenject;
using Configs;
using Level;
using Managers;

namespace Enemy
{
    public class RedEnemyController : EnemyController
    {
        public class Factory : PlaceholderFactory<RedEnemyConfig, RedEnemyController>
        {
            
        }
        
        [Inject]
        public void Construct(RedEnemyConfig enemyConfig, LevelController levelController,
            Fighter playerFighter)
        {
            _enemyConfig = enemyConfig;
            _playerFighter = playerFighter;
            _levelController = levelController;
        }
    }
}