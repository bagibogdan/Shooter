using Configs.Weapons;
using Zenject;

namespace Weapons
{
    public class Ak47Weapon : Weapon
    {
        [Inject]
        public void Construct(Ak47Config config)
        {
            _weaponConfig = config;
        }
    }
}