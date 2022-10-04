using Configs.Weapons;
using Zenject;

namespace Weapons
{
    public class M16Weapon : Weapon
    {
        [Inject]
        public void Construct(M16Config config)
        {
            _weaponConfig = config;
        }
    }
}