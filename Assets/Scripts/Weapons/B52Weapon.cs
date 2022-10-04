using Configs.Weapons;
using Zenject;

namespace Weapons
{
    public class B52Weapon : Weapon
    {
        [Inject]
        public void Construct(B52Config config)
        {
            _weaponConfig = config;
        }
    }
}