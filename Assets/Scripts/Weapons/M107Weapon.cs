using Configs.Weapons;
using Zenject;

namespace Weapons
{
    public class M107Weapon : Weapon
    {
        [Inject]
        public void Construct(M107Config config)
        {
            _weaponConfig = config;
        }
    }
}