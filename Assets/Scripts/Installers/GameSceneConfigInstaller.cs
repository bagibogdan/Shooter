using Configs;
using Configs.Weapons;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneConfigInstaller", menuName = "Installers/GameSceneConfigInstaller")]
public class GameSceneConfigInstaller : ScriptableObjectInstaller<GameSceneConfigInstaller>
{
    [SerializeField] private PlayerConfig playerConfig;
    [SerializeField] private EnemyConfig enemyConfig;
    [SerializeField] private M16Config m16Config;
    
    public override void InstallBindings()
    {
        Container.BindInstance(playerConfig);
        Container.BindInstance(enemyConfig);
        Container.BindInstance(m16Config);
    }
}