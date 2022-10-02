using Configs;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneConfigInstaller", menuName = "Installers/GameSceneConfigInstaller")]
public class GameSceneConfigInstaller : ScriptableObjectInstaller<GameSceneConfigInstaller>
{
    [SerializeField] private PlayerConfig playerConfig;
    public override void InstallBindings()
    {
        Container.BindInstance(playerConfig);
    }
}