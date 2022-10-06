using Zenject;
using Configs;
using Level;
using Managers;

namespace Enemy
{
    public class YellowEnemyController : EnemyController
    {
        public class Factory : PlaceholderFactory<YellowEnemyConfig, YellowEnemyController>
        {
            
        }
        
        [Inject]
        public void Construct(YellowEnemyConfig enemyConfig, LevelController levelController,
            Fighter playerFighter)
        {
            _enemyConfig = enemyConfig;
            _playerFighter = playerFighter;
            _levelController = levelController;
        }
    }
}