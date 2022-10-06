using Configs;
using Configs.Weapons;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "ProjectConfigInstaller", menuName = "Installers/ProjectConfigInstaller")]
public class ProjectConfigInstaller : ScriptableObjectInstaller<ProjectConfigInstaller>
{
    [Header("Enemies:")]
    [SerializeField] private YellowEnemyConfig yellowEnemyConfig;
    [SerializeField] private WhiteEnemyConfig whiteEnemyConfig;
    [SerializeField] private RedEnemyConfig redEnemyConfig;
    
    [Header("Weapons:")]
    [SerializeField] private Ak47Config ak47Config;
    [SerializeField] private B52Config b52Config;
    [SerializeField] private M4Config m4Config;
    [SerializeField] private M16Config m16Config;
    [SerializeField] private M107Config m107Config;
    [SerializeField] private M249Config m249Config;
    
    public override void InstallBindings()
    {
        Container.BindInstances(yellowEnemyConfig, whiteEnemyConfig, redEnemyConfig);
        
        Container.BindInstance(ak47Config);
        Container.BindInstance(b52Config);
        Container.BindInstance(m4Config);
        Container.BindInstance(m16Config);
        Container.BindInstance(m107Config);
        Container.BindInstance(m249Config);

    }
}