using Configs.Weapons;
using Zenject;

namespace Weapons
{
    public class BennelliWeapon : Weapon
    {
        [Inject]
        public void Construct(BennelliConfig config)
        {
            _weaponConfig = config;
        }
    }
}