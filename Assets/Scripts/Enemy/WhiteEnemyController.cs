using Zenject;
using Configs;
using Level;

namespace Enemy
{
    public class WhiteEnemyController : EnemyController
    {
        [Inject]
        public void Construct(SignalBus signalBus,
            WhiteEnemyConfig enemyConfig,
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