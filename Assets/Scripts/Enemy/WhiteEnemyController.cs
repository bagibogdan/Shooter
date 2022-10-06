using Zenject;
using Configs;
using Level;
using Managers;

namespace Enemy
{
    public class WhiteEnemyController : EnemyController
    {
        public class Factory : PlaceholderFactory<WhiteEnemyConfig, WhiteEnemyController>
        {
            
        }
        
        [Inject]
        public void Construct(WhiteEnemyConfig enemyConfig, LevelController levelController,
            Fighter playerFighter)
        {
            _enemyConfig = enemyConfig;
            _playerFighter = playerFighter;
            _levelController = levelController;
        }
    }
}