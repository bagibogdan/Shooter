using Zenject;
using Configs;
using Level;

namespace Enemy
{
    public class YellowEnemyController : EnemyController
    {
        [Inject]
        public void Construct(SignalBus signalBus,
            YellowEnemyConfig enemyConfig,
            LevelController levelController,
            Fighter playerFighter)
        {
            _enemyConfig = enemyConfig;
            _signalBus = signalBus;
            _playerFighter = playerFighter;
            _levelController = levelController;
        }
    }
}