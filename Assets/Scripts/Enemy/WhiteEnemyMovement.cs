using Zenject;
using Configs;
using Player;

namespace Enemy
{
    public class WhiteEnemyMovement : EnemyMovement
    {
        [Inject]
        public void Construct(WhiteEnemyConfig enemyConfig, PlayerMovement playerMovement)
        {
            _enemyConfig = enemyConfig;
            _player = playerMovement.gameObject.transform;
        }
    }
}