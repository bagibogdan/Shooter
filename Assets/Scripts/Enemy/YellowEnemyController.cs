using Zenject;
using Configs;
using Player;
using Level;

namespace Enemy
{
    public class YellowEnemyController : EnemyController
    {
        [Inject]
        public void Construct(SignalBus signalBus, YellowEnemyConfig enemyConfig, 
            PlayerController playerController, LevelController levelController, Fighter playerFighter)
        {
            _enemyConfig = enemyConfig;
            _signalBus = signalBus;
            _playerFighter = playerFighter;
            _levelController = levelController;
        }
    }
}