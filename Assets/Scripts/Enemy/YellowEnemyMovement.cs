using Zenject;
using Configs;

namespace Enemy
{
    public class YellowEnemyMovement : EnemyMovement
    {
        [Inject]
        public void Construct(YellowEnemyConfig enemyConfig)
        {
            _enemyConfig = enemyConfig;
        }
    }
}