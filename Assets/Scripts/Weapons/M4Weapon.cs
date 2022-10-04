using Configs.Weapons;
using Zenject;

namespace Weapons
{
    public class M4Weapon : Weapon
    {
        [Inject]
        public void Construct(M4Config config)
        {
            _weaponConfig = config;
        }
    }
}