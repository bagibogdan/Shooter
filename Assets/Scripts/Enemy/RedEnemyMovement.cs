using Zenject;
using Configs;
using Player;

namespace Enemy
{
    public class RedEnemyMovement : EnemyMovement
    {
        [Inject]
        public void Construct(RedEnemyConfig enemyConfig, PlayerMovement playerMovement)
        {
            _enemyConfig = enemyConfig;
            _player = playerMovement.gameObject.transform;
        }
    }
}