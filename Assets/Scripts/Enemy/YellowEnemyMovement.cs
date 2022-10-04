using Zenject;
using Configs;
using Level;

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