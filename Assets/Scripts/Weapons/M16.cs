using Configs.Weapons;
using Zenject;

namespace Weapons
{
    public class M16 : Weapon
    {
        [Inject]
        public void Construct(M16Config m16Config)
        {
            _weaponConfig = m16Config;
        }
    }
}