using Configs.Weapons;
using Zenject;

namespace Weapons
{
    public class M249Weapon : Weapon
    {
        [Inject]
        public void Construct(M249Config config)
        {
            _weaponConfig = config;
        }
    }
}