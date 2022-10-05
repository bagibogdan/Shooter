using Zenject;
using Configs;
using Player;

namespace Enemy
{
    public class YellowEnemyMovement : EnemyMovement
    {
        [Inject]
        public void Construct(YellowEnemyConfig enemyConfig, PlayerMovement playerMovement)
        {
            _enemyConfig = enemyConfig;
            _player = playerMovement.gameObject.transform;
        }
    }
}